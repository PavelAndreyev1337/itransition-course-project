using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Interfaces;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CollectionApp.BLL.Utils;
using System.Threading.Tasks;
using CollectionApp.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using CollectionApp.DAL.DTO;
using HeyRed.MarkdownSharp;
using CollectionApp.BLL.Enums;

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

        [AllowAnonymous]
        [Route("/Collections/{collectionId}/Items/", Name = "Items")]
        public async Task<IActionResult> Index(
            [FromRoute(Name = "collectionId")] int collectionId,
            [FromQuery] int page = 1,
            [FromQuery] string userId = "",
            [FromQuery] ItemSort sortOrder = ItemSort.Default,
            [FromQuery] bool isLiked = false,
            [FromQuery] bool isCommented = false,
            [FromQuery] string backController = "",
            [FromQuery] string backAction = "")
        {
            var cookies = Response.Cookies;
            cookies.Append("itemPage", page.ToString());
            ViewData["collectionId"] = collectionId.ToString();
            cookies.Append("collectionId", collectionId.ToString());
            cookies.Append("sortOrder", sortOrder.ToString());
            cookies.Append("isLiked", isLiked.ToString());
            cookies.Append("isCommented", isCommented.ToString());
            ViewData["userId"] = userId;
            ViewData["controller"] = backController;
            ViewData["action"] = backAction;
            var sd = await _itemService
                .GetItems(collectionId, User, page, sortOrder, isLiked, isCommented);
            return View(sd);
        }

        public async Task<IActionResult> Create(
            [FromQuery] int collectionId,
            [FromQuery] string userId = "")
        {
            var item = new ItemViewModel();
            var collectionDto = await _collectionService.GetCollection(collectionId);
            item.Collection = MapperUtil.Map<CollectionDTO, Collection>(collectionDto);
            ViewData["userId"] = userId;
            return View("Form", item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel model, [FromQuery] string userId = "")
        {
            var itemDto = MapperUtil.Map<ItemViewModel, ItemDTO>(model);
            await _itemService.CreateItem(User, itemDto, userId);
            return RedirectToAction(
                "Index",
                new { collectionId = model.CollectionId, page = 1, userId = userId });
        }

        [Route("/Tags")]
        public EntityPageDTO<Tag> GetTags(string input)
        {
            return _itemService.GetTags(input);
        }

        public async Task<IActionResult> Edit(int itemId, [FromQuery] string userId = "")
        {
            var itemDto = await _itemService.GetItem(itemId);
            ViewData["userId"] = userId;
            return View("Form", MapperUtil.Map<ItemDTO, ItemViewModel>(itemDto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemViewModel model, [FromQuery] string userId = "")
        {
            await _itemService.EditItem(
                User,
                MapperUtil.Map<ItemViewModel, ItemDTO>(model));
            return RedirectToAction(
                "Index",
                new { collectionId = model.CollectionId, page = 1, userId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int itemId, [FromQuery] string userId = "")
        {
            var collectionId = await _itemService.DeleteItem(User, itemId);
            return RedirectToAction(
                "Index",
                new { collectionId = collectionId, page = 1, userId = userId });
        }

        [AllowAnonymous]
        [Route("/Items/{itemId}")]
        public async Task<IActionResult> GetItem(
            [FromRoute(Name = "itemId")] int itemId,
            [FromQuery] string userId = "",
            [FromQuery] int page = 1)
        {
            var itemDto = await _itemService.GetItem(itemId, page, User);
            var model = MapperUtil.Map<ItemDTO, ItemViewModel>(itemDto);
            var markdown = new Markdown();
            model.FirstText = markdown.Transform(model.FirstText ?? "");
            model.SecondText = markdown.Transform(model.SecondText ?? "");
            model.ThirdText = markdown.Transform(model.ThirdText ?? "");
            if (User.Identity.IsAuthenticated)
            {
                Response.Cookies.Append("username", User.Identity.Name);
            }
            Response.Cookies.Append("itemId", itemId.ToString());
            ViewData["Action"] = "GetItem";
            ViewData["userId"] = userId;
            return View("Item", model);
        }

        [HttpPost]
        [Route("/Like")]
        public async Task<LikeViewModel> LikeItem(int itemId)
        {
            var likeDto = await _itemService.LikeItem(User, itemId);
            return MapperUtil.Map<LikeDTO, LikeViewModel>(likeDto);
        }

        [AllowAnonymous]
        [Route("/Items")]
        public IActionResult GetItemsByTag([FromQuery] string tag)
        {
            return View("Search", _itemService.GetItemsByTag(tag));
        }
    }
}
