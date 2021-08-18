using CollectionApp.BLL.Interfaces;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CollectionApp.WEB.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(_collectionService.GetTopics());
        }

        [HttpPost]
        public IActionResult Create(CollectionViewModel model)
        {
            return RedirectToAction("Index", "Account");
        }
    }
}
