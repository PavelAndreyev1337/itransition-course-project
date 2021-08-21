using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Interfaces;
using CollectionApp.BLL.Utils;
using CollectionApp.WEB.ViewModels;
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [Route("/Profile", Name = "Profile")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var collections = await _collectionService.GetUserCollections(User, page);
            var markdown = new Markdown();
            foreach(var entity in collections.Entities)
            {
                entity.ShortDescription = markdown.Transform(entity.ShortDescription);
            }
            Response.Cookies.Append("page", page.ToString());
            return View(collections);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Response.Cookies.Delete("collectionId");
            return View("Form", new CollectionViewModel
            {
                Topics = _collectionService.GetTopics()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionViewModel model)
        {
            await _collectionService.CreateCollection(
                User,
                MapperUtil.Map<CollectionViewModel, CollectionDTO>(model));
            return RedirectToAction("Index");
        }

        [HttpGet("/Collection/Edit")]
        public async Task<IActionResult> EditCollection([FromQuery(Name = "collectionId")] int collectionId)
        {
            try
            {
                var collectionDto = await _collectionService.GetUserCollection(User, collectionId);
                Response.Cookies.Append("collectionId", collectionId.ToString());
                return View("Form", MapperUtil.Map<CollectionDTO, CollectionViewModel>(collectionDto));
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("/Collections/{collectionId}/Images")]
        public IEnumerable<string> GetImages(int collectionId)
        {
            return _collectionService.GetImages(collectionId);
        }
       
        [HttpPost]
        public async Task<IActionResult> Edit(
            [FromQuery(Name = "collectionId")] int collectionId,
            CollectionViewModel model)
        {
            try
            {
                model.Id = collectionId;
                await _collectionService.EditCollection(
                    User,
                    MapperUtil.Map<CollectionViewModel, CollectionDTO>(model));
                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _collectionService.Dispose();
            base.Dispose(disposing);
        }
    }
}
