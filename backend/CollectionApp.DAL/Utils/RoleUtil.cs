using CollectionApp.DAL.Enums;
using System.Security.Claims;

namespace CollectionApp.DAL.Utils
{
    public class RoleUtil
    {
        public static string AdminRoleName()
        {
            return Role.Admin.ToString().ToLower();
        }

        public static bool IsAdmin(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(AdminRoleName());
        }
    }
}
