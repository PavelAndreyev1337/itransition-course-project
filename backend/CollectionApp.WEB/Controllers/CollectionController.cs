using AutoMapper;
using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Interfaces;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CollectionApp.WEB.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create(CollectionViewModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CollectionViewModel, CollectionDTO>());
            var mapper = new Mapper(config);
            await _collectionService.CreateCollection(User, mapper.Map<CollectionViewModel, CollectionDTO>(model));
            return RedirectToAction("Index", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            _collectionService.Dispose();
            base.Dispose(disposing);
        }
    }
}
