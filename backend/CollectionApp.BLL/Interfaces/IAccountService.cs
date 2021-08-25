using CollectionApp.BLL.DTO;
using CollectionApp.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface IAccountService
    {
        AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl);
        Task<User> GetCurrentUser(ClaimsPrincipal userPrincipal);
        Task<AccountDTO> Login();
        Task Logout();
    }
}
