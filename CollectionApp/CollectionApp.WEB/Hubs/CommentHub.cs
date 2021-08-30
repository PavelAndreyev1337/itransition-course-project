using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CollectionApp.WEB.Hubs
{
    [Authorize]
    public class CommentHub : Hub
    {
        private readonly IItemService _itemService;

        public CommentHub(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task JoinRoom(string itemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "item-"+itemId);
        }

        public async Task Send(string itemId, string comment)
        {
            await _itemService.AddComment(
                Context.User,
                new CommentDTO()
                {
                    ItemId = int.Parse(itemId),
                    Text = comment
                });
            await Clients.GroupExcept("item-" + itemId, Context.ConnectionId)
                .SendAsync("Receive", Context.User.Identity.Name, comment);
        }
    }
}
