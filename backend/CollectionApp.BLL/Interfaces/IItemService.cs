using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Enums;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface IItemService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<EntityPageDTO<Item>> GetItems(
            int collectionId,
            ClaimsPrincipal userPrincipal,
            int page=1,
            ItemSort sortState = ItemSort.Default,
            bool isLiked = false,
            bool isCommented = false);
        Task CreateItem(ClaimsPrincipal userPrincipal, ItemDTO itemDto);
        EntityPageDTO<Tag> GetTags(string input);
        Task<ItemDTO> GetItem(int itemId, int page = 1, ClaimsPrincipal claimsPrincipal=null);
        Task EditItem(ClaimsPrincipal claimsPrincipal, ItemDTO itemDto);
        Task<int> DeleteItem(ClaimsPrincipal claimsPrincipal, int itemId);
        Task<LikeDTO> LikeItem(ClaimsPrincipal claimsPrincipal, int itemId);
        Task AddComment(ClaimsPrincipal claimsPrincipal, CommentDTO commentDto);
    }
}
