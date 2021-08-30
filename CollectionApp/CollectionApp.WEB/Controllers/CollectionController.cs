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

        [Route("/Collections", Name = "Profile")]
        public async Task<IActionResult> Index(
            [FromQuery] int page = 1,
            [FromQuery] string userId = "")
        {
            var collections = await _collectionService.GetUserCollections(User, page, userId);
            var markdown = new Markdown();
            foreach(var entity in collections.Entities)
            {
                entity.ShortDescription = markdown.Transform(entity.ShortDescription);
            }
            ViewData["userId"] = userId;
            Response.Cookies.Append("collectionPage", page.ToString());
            return View(collections);
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string userId = "")
        {
            Response.Cookies.Delete("collectionId");
            ViewData["userId"] = userId;
            return View("Form", new CollectionViewModel
            {
                Topics = _collectionService.GetTopics()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionViewModel model, [FromQuery] string userId = "")
        {
            await _collectionService.CreateCollection(
                User,
                MapperUtil.Map<CollectionViewModel, CollectionDTO>(model),
                userId);
            return RedirectToAction("Index", new { userId = userId});
        }

        [HttpGet("/Collection/Edit")]
        public async Task<IActionResult> EditCollection(
            [FromQuery(Name = "collectionId")] int collectionId,
            [FromQuery] string userId = "")
        {
            try
            {
                var collectionDto = await _collectionService.GetCollection(collectionId);
                Response.Cookies.Append("collectionId", collectionId.ToString());
                var collection = MapperUtil.Map<CollectionDTO, CollectionViewModel>(collectionDto);
                await _collectionService.CheckRights(User, collectionId);
                ViewData["userId"] = userId;
                return View("Form", collection);
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
            CollectionViewModel model,
            [FromQuery(Name = "collectionId")] int collectionId,
            [FromQuery] string userId = "")
        {
            try
            {
                model.Id = collectionId;
                await _collectionService.EditCollection(
                    User,
                    MapperUtil.Map<CollectionViewModel, CollectionDTO>(model));
                return RedirectToAction("Index", new { userId = userId });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(
            [FromQuery(Name = "collectionId")] int collectionId,
            [FromQuery] string userId = "")
        {
            try
            {
                await _collectionService.DeleteCollection(User, collectionId);
                return RedirectToAction("Index", new { userId = userId });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
