using CollectionApp.BLL.BusinessModels;
using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Interfaces;
using CollectionApp.BLL.Utils;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Services
{
    public class ItemService : IItemService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public ICollectionService _collectionService { get; set; }

        public ItemService(IUnitOfWork unitOfWork, ICollectionService collectionService)
        {
            UnitOfWork = unitOfWork;
            _collectionService = collectionService;
        }

        public async Task<EntityPageDTO<Item>> GetItems(int collectionId)
        {
            var collection = await UnitOfWork.Collections.Get(collectionId);
            return await UnitOfWork.Items.Paginate(
                predicate: item => item.CollectionId == collection.Id,
                includes: item => item.Tags);
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
        
        public async Task<EntityPageDTO<Tag>> GetTags(string input)
        {
            return await UnitOfWork.Tags.Paginate(predicate: tag => tag.Name.Contains(input));
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
