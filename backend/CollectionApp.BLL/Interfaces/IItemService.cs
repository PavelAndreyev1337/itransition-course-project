using CollectionApp.BLL.DTO;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface IItemService
    {
        IUnitOfWork UnitOfWork { get; set; }
        public Task<EntityPageDTO<Item>> GetItems(int collectionId);
        public Task CreateItem(ClaimsPrincipal userPrincipal, ItemDTO itemDto);
        public Task<EntityPageDTO<Tag>> GetTags(string input);
        public Task<ItemDTO> GetItem(int itemId);
    }
}
