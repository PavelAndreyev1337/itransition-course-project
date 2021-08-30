using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CollectionApp.DAL.Utils;

namespace CollectionApp.DAL.Initializers
{
    public class AdminInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleName = RoleUtil.AdminRoleName();
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
