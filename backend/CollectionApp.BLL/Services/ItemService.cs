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
            return await UnitOfWork.Items.Paginate(predicate: item => item.CollectionId == collection.Id);
        }

        public async Task CreateItem(ClaimsPrincipal userPrincipal, ItemDTO itemDto)
        {
            await _collectionService.CheckRights(userPrincipal, (int)(itemDto.CollectionId));
            var tags = JsonSerializer.Deserialize<IEnumerable<TagBusinessModel>>(itemDto.TagsJson);
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var item = MapperUtil.Map<ItemDTO, Item>(itemDto);
                UnitOfWork.Items.Add(item);
                await UnitOfWork.SaveAsync();
                foreach (var newTag in tags)
                {
                    var existingTags = UnitOfWork.Tags.Find(tag => tag.Name == newTag.value).ToList();
                    if (existingTags.Count() == 0)
                    {
                        item.Tags.Add(new Tag() { Name = newTag.value });
                    }
                    else
                    {
                        item.Tags.Add(existingTags.First());
                    }
                }
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();
            }
        }
        
        public async Task<EntityPageDTO<Tag>> GetTags(string input)
        {
            return await UnitOfWork.Tags.Paginate(predicate: tag => tag.Name.Contains(input));
        }
    }
}
