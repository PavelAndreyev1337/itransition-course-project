using CollectionApp.BLL.Exceptions;
using CollectionApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CollectionApp.WEB.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult SetLogin(string provider)
        {
            var redirectUrl = Url.Action("LoginOauth", "Account");
            return new ChallengeResult(provider,
                _accountService.GetAuthenticationProperties(provider, redirectUrl));
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginOauth()
        {
            try
            {
                await _accountService.Login();
                return RedirectToAction("Index", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
