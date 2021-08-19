using CollectionApp.BLL.DTO;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface ICollectionService
    {
        public IEnumerable<string> GetTopics();
        public Task CreateCollection(ClaimsPrincipal user, CollectionDTO collectionDto);
        void Dispose();
    }
}
