using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Interfaces;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CollectionApp.BLL.Utils;
using System.Threading.Tasks;
using CollectionApp.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using CollectionApp.DAL.DTO;

namespace CollectionApp.WEB.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICollectionService _collectionService;

        public ItemController(
            IItemService itemService,
            ICollectionService collectionService)
        {
            _itemService = itemService;
            _collectionService = collectionService;
        }

        [Route("/Collections/{collectionId}/Items/{page}", Name = "Items")]
        public async Task<IActionResult> Index(int collectionId, int page=1)
        {
            Response.Cookies.Append("itemPage", page.ToString());
            Response.Cookies.Append("collectionId", collectionId.ToString());
            return View(await _itemService.GetItems(collectionId));
        }

        public async Task<IActionResult> Create(int collectionId)
        {
            var item = new ItemViewModel();
            var collectionDto = await _collectionService.GetCollection(collectionId);
            item.Collection = MapperUtil.Map<CollectionDTO, Collection>(collectionDto);
            return View("Form", item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel model)
        {
            var itemDto = MapperUtil.Map<ItemViewModel, ItemDTO>(model);
            await _itemService.CreateItem(User, itemDto);
            return RedirectToAction("Index", new { collectionId = model.CollectionId, page=1});
        }

        [Route("/Tags")]
        public async Task<EntityPageDTO<Tag>> GetTags(string input)
        {
            return await _itemService.GetTags(input);
        }

        public async Task<IActionResult> Edit(int itemId)
        {
            var itemDto = await _itemService.GetItem(itemId);
            return View("Form", MapperUtil.Map<ItemDTO, ItemViewModel>(itemDto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemViewModel model)
        {
            await _itemService.EditItem(
                User,
                MapperUtil.Map<ItemViewModel, ItemDTO>(model));
            return RedirectToAction("Index", new { collectionId = model.CollectionId, page = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int itemId)
        {
            var collectionId = await _itemService.DeleteItem(User, itemId);
            return RedirectToAction("Index", new { collectionId = collectionId, page = 1 });
        }
    }
}
