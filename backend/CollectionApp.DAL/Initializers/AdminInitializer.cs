using CollectionApp.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Initializers
{
    public class AdminInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleName = Role.Admin.ToString().ToLower();
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
