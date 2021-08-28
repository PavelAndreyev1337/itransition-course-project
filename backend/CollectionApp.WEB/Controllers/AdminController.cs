using CollectionApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CollectionApp.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index(int page = 1)
        {
            Response.Cookies.Append("adminPage", page.ToString());
            return View( _adminService.GetUsers(User, page));
        }

        public async Task<IActionResult> AddAdmin(string userId)
        {
            await _adminService.AddAdmin(userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BlockUser(string userId)
        {
            await _adminService.BlockUser(userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _adminService.DeleteUser(userId);
            return RedirectToAction("Index");
        }
    }
}
