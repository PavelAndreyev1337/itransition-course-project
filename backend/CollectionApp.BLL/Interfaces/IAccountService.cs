using CollectionApp.BLL.DTO;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface IAccountService
    {
        AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl);
        Task<AccountDTO> Login();
        Task Logout();
    }
}
