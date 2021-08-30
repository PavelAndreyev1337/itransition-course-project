using CollectionApp.DAL.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetUsers(ClaimsPrincipal claimsPrincipal);
        Task AddAdmin(string userId);
        Task BlockUser(string userId);
        Task DeleteUser(string userId);
    }
}
