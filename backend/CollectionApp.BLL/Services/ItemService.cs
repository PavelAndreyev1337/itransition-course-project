using AutoMapper;
using CollectionApp.BLL.BusinessModels;
using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Enums;
using CollectionApp.BLL.Interfaces;
using CollectionApp.BLL.Utils;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Extensions;
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
            var currentUser = await _accountService.GetCurrentUser(userPrincipal);
            var collection = await UnitOfWork.Collections.Get(collectionId);
            Func<Item, bool> predicate = item => item.CollectionId == collection.Id;
            if (currentUser != null && (isLiked || isCommented))
            {
                predicate = item =>
                {
                    var expression = item.CollectionId == collection.Id;
                    if (isLiked)
                    {
                        expression &= item.UsersLiked.Contains(currentUser);
                    }
                    if (isCommented)
                    {
                        expression &= item.Comments.Any(
                            comment => comment.UserId == currentUser.Id);
                    }
                    return expression;
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
                sortPredicate: sortPredicate,
                includes: new Expression<Func<Item, object>>[] {
                    item => item.Tags,
                    item => item.Comments,
                    item => item.UsersLiked
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

        public async Task CreateItem(
            ClaimsPrincipal userPrincipal,
            ItemDTO itemDto,
            string userId = "")
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

        public async Task<ItemDTO> GetItem(
            int itemId,
            int page = 1,
            ClaimsPrincipal claimsPrincipal=null)
        {
            var item = await UnitOfWork.Items.Get(
                itemId,
                item => item.Collection,
                item => item.UsersLiked,
                item => item.Tags);
            var mapperConf = new MapperConfiguration(
                    cfg => cfg.CreateMap<Item, ItemDTO>()
                    .ForMember(item => item.Comments, opt => opt.Ignore()));
            var itemDto = MapperUtil.Map<Item, ItemDTO>(item, conf: mapperConf);
            itemDto.Comments = UnitOfWork.Comments.Paginate(page: page,
                predicate: comment => item.Comments.Contains(comment),
                includes: new Expression<Func<Comment, object>>[] {
                    comment => comment.User
                });
            if (claimsPrincipal != null)
            {
                var user = await _accountService.GetCurrentUser(claimsPrincipal);
                itemDto.Liked = item.UsersLiked.Contains(user);
            }
            var tags = item.Tags.Select(
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

        public async Task<LikeDTO> LikeItem(ClaimsPrincipal claimsPrincipal, int itemId)
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal);
            var item = await UnitOfWork.Items.Get(itemId, item => item.UsersLiked);
            var likeDto = new LikeDTO();
            if (item.UsersLiked.Contains(user))
            {
                item.UsersLiked.Remove(user);
                likeDto.Liked = false;
                item.Likes -= 1;
            }
            else
            {
                item.UsersLiked.Add(user);
                likeDto.Liked = true;
                item.Likes += 1;
            }
            likeDto.Count = item.Likes;
            await UnitOfWork.SaveAsync();
            return likeDto;
        }

        public async Task AddComment(ClaimsPrincipal claimsPrincipal, CommentDTO commentDto)
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal);
            var item = await UnitOfWork.Items.Get(commentDto.ItemId);
            UnitOfWork.Comments.Add(new Comment() 
            { 
                ItemId = item.Id,
                Text = commentDto.Text,
                UserId = user.Id
            });
            await UnitOfWork.SaveAsync();
        }

        public IEnumerable<Item> GetLastCreatedItems()
        {
            return UnitOfWork.Context.Items.IncludeMultiple(
                item => item.UsersLiked,
                item => item.Tags,
                Item => Item.Collection)
                .AsEnumerable()
                .TakeLast(3)
                .Reverse();
        }
    }
}
