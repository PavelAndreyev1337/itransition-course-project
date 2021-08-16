using CollectionApp.BLL.DTO;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface IAccountService
    {
        AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl);
        public Task<AccountDTO> Login();
        public Task Logout();
    }
}
