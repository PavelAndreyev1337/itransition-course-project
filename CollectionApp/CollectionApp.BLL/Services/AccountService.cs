using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Exceptions;
using CollectionApp.BLL.Interfaces;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using CollectionApp.DAL.Utils;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWork Database { get; set; }

        public AccountService(IUnitOfWork database)
        {
            Database = database;
        }

        public AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl)
        {
            return Database.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<AccountDTO> Login()
        {
            var info = await Database.SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                throw new UserNotLoggedException();
            }
            var result = await Database.SignInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            var userInfo = new AccountDTO
            {
                Name = info.Principal.FindFirst(ClaimTypes.Name).Value,
                Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value
            };
            if (result.Succeeded)
            {
                return userInfo;
            }
            else
            {
                var user = new User
                {
                    UserName = info.Principal.FindFirst(ClaimTypes.Name).Value,
                    Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value
                };
                var identityResult = await Database.UserManager.CreateAsync(user);
                if (identityResult.Succeeded)
                {
                    identityResult = await Database.UserManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded)
                    {
                        await Database.SignInManager.SignInAsync(user, false);
                        return userInfo;
                    }
                }
                throw new AccessDeniedException();
            }
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal userPrincipal, string userId = "")
        {
            if (!string.IsNullOrEmpty(userId) && RoleUtil.IsAdmin(userPrincipal))
            {
                return await Database.UserManager.FindByIdAsync(userId);
            }
            return await Database.UserManager.GetUserAsync(userPrincipal);
        }

        public async Task Logout()
        {
            await Database.SignInManager.SignOutAsync();
        }
    }
}
