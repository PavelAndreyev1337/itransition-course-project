using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Utils;
using System.Security.Claims;

namespace CollectionApp.BLL.Utils
{
    public class AuthUtil
    {
        public static bool CheckRights(
            ClaimsPrincipal claimsPrincipal,
            Collection collection)
        {
            return claimsPrincipal.Identity.IsAuthenticated 
                && (claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) == collection.User.Id 
                || RoleUtil.IsAdmin(claimsPrincipal));
        }
    }
}
