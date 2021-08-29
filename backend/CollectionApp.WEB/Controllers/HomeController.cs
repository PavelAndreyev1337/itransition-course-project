using CollectionApp.BLL.Interfaces;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CollectionApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICollectionService _collectionService;

        public HomeController(IItemService itemService, ICollectionService collectionService)
        {
            _itemService = itemService;
            _collectionService = collectionService;
        }
        public IActionResult Index()
        {
            return View(new HomeViewModel()
            { 
                LastCreatedItems = _itemService.GetLastCreatedItems(),
                LagestNumberItems = _collectionService.GetLagestNumberItems()
            });
        }

        public IActionResult GetAllCollections(int page = 1)
        {
            ViewData["Action"] = "GetAllCollections";
            return View("../Collection/Index", _collectionService.GetAllCollections(page));
        }
    }
}
