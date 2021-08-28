using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface IAdminService
    {
        EntityPageDTO<User> GetUsers(ClaimsPrincipal claimsPrincipal, int page = 1);
        Task AddAdmin(string userId);
        Task BlockUser(string userId);
        Task DeleteUser(string userId);
    }
}
