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
        Task<EntityPageDTO<Item>> GetItems(int collectionId, int page=1);
        Task CreateItem(ClaimsPrincipal userPrincipal, ItemDTO itemDto);
        Task<EntityPageDTO<Tag>> GetTags(string input);
        Task<ItemDTO> GetItem(int itemId);
        Task EditItem(ClaimsPrincipal claimsPrincipal, ItemDTO itemDto);
        Task<int> DeleteItem(ClaimsPrincipal claimsPrincipal, int itemId);
    }
}
