using CollectionApp.BLL.BusinessModels;
using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Enums;
using CollectionApp.BLL.Interfaces;
using CollectionApp.BLL.Utils;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Enums;
using CollectionApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Services
{
    public class ItemService : IItemService
    {
        public IUnitOfWork UnitOfWork { get; }

        private IAccountService _accountService;

        private ICollectionService _collectionService;

        public ItemService(
            IUnitOfWork unitOfWork,
            IAccountService accountService,
            ICollectionService collectionService)
        {
            UnitOfWork = unitOfWork;
            _accountService = accountService;
            _collectionService = collectionService;
        }

        public async Task<EntityPageDTO<Item>> GetItems(
            int collectionId,
            ClaimsPrincipal userPrincipal,
            int page= 1,
            ItemSort sortState = ItemSort.Default,
            bool isLiked = false,
            bool isCommented = false)
        {
            var user = await _accountService.GetCurrentUser(userPrincipal);
            var collection = await UnitOfWork.Collections.Get(collectionId);
            Func<Item, bool> predicate = item => item.CollectionId == collection.Id;
            if (user != null && isLiked || isCommented)
            {
                predicate = item =>
                {
                    return item.CollectionId == collection.Id && (isLiked && item.UsersLiked.Contains(user)
                        || isCommented && item.Comments.Any(
                            comment => comment.UserId == user.Id));
                };
            }
            Func<Item, object> sortPredicate = null;
            if (sortState == ItemSort.LikeDesc)
            {
                sortPredicate = item => item.UsersLiked.Count();
            }
            return UnitOfWork.Items.Paginate(
                page: page,
                predicate: predicate,
                sort: Sort.Desc,
                sortPredicate: sortPredicate,
                includes: new Expression<Func<Item, object>>[] {
                    item => item.Tags,
                    item => item.Comments
                });
        }

        private IEnumerable<TagBusinessModel> DeserializeTags(string json)
        {
            return JsonSerializer.Deserialize<IEnumerable<TagBusinessModel>>(json);
        }

        private async Task AddTags(Item model, IEnumerable<TagBusinessModel> tags)
        {
            foreach (var newTag in tags)
            {
                var existingTags = UnitOfWork.Tags.Find(tag => tag.Name == newTag.value).ToList();
                if (existingTags.Count() == 0)
                {
                    model.Tags.Add(new Tag() { Name = newTag.value });
                }
                else
                {
                    model.Tags.Add(existingTags.First());
                }
            }
            await UnitOfWork.SaveAsync();
        }

        public async Task CreateItem(ClaimsPrincipal userPrincipal, ItemDTO itemDto)
        {
            await _collectionService.CheckRights(userPrincipal, (int)(itemDto.CollectionId));
            var tags = DeserializeTags(itemDto.TagsJson);
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var model = MapperUtil.Map<ItemDTO, Item>(itemDto);
                UnitOfWork.Items.Add(model);
                await UnitOfWork.SaveAsync();
                await AddTags(model, tags);
                await transaction.CommitAsync();
            }
        }
        
        public EntityPageDTO<Tag> GetTags(string input)
        {
            return UnitOfWork.Tags.Paginate(predicate: tag => tag.Name.Contains(input));
        }

        public async Task<ItemDTO> GetItem(int itemId)
        {
            var item = await UnitOfWork.Items.Get(
                itemId,
                item => item.Collection,
                item => item.Tags);;
            var itemDto = MapperUtil.Map<Item, ItemDTO>(item);
            var tags = item.Tags.ToList().Select(
                item => new TagBusinessModel() { value = item.Name });
            itemDto.TagsJson = JsonSerializer.Serialize<IEnumerable<TagBusinessModel>>(tags);
            return itemDto;
        }

        public async Task EditItem(ClaimsPrincipal claimsPrincipal, ItemDTO itemDto)
        {
            await _collectionService.CheckRights(claimsPrincipal, (int)(itemDto.CollectionId));
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var model = MapperUtil.Map<ItemDTO, Item>(itemDto);
                UnitOfWork.Items.Update(model);
                await UnitOfWork.SaveAsync();
                model = await UnitOfWork.Items.Get(model.Id, item => item.Tags);
                var tags = DeserializeTags(itemDto.TagsJson);
                model.Tags.Clear();
                await AddTags(model, tags);
                await transaction.CommitAsync();
            }
        }

        public async Task<int> DeleteItem(ClaimsPrincipal claimsPrincipal, int itemId)
        {
            var item = await GetItem(itemId);
            var collectionId = (int)item.CollectionId;
            await _collectionService.CheckRights(claimsPrincipal, collectionId);
            await UnitOfWork.Items.Delete(itemId);
            await UnitOfWork.SaveAsync();
            return collectionId;
        }
    }
}
