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
        public IEnumerable<string> GetTopics();
        public Task CreateCollection(ClaimsPrincipal user, CollectionDTO collectionDto);
        public Task<EntityPageDTO<Collection>> GetUserCollection(ClaimsPrincipal claimsPrincipal, int page=1);
        void Dispose();
    }
}
