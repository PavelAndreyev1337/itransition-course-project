using CollectionApp.BLL.DTO;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface ICollectionService
    {
        IEnumerable<string> GetTopics();
        Task CreateCollection(
            ClaimsPrincipal user,
            CollectionDTO collectionDto,
            string userId = "");
        Task<Collection> CheckRights(
            ClaimsPrincipal claimsPrincipal,
            int collectionId,
            string userId = "");
        Task<EntityPageDTO<Collection>> GetUserCollections(
            ClaimsPrincipal claimsPrincipal,
            int page=1,
            string userId = "");
        Task<CollectionDTO> GetCollection(int collectionId);
        IEnumerable<string> GetImages(int collectionId);
        Task EditCollection(ClaimsPrincipal claimsPrincipal, CollectionDTO collectionDto);
        Task DeleteCollection(ClaimsPrincipal claimsPrincipal, int collectionId);
        void Dispose();
    }
}
