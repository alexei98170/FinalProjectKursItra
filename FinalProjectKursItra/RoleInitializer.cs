using FinalProjectKursItra.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FinalProjectKursItra
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("author") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("author"));
            }
            if (await roleManager.FindByNameAsync("member") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("member"));
            }
        }
    }
}
