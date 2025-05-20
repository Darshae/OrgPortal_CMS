using Microsoft.AspNetCore.Identity;
using OrgPortal_CMS.Areas.Identity.Data;

namespace OrgPortal_CMS.Services
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<OrgPortal_CMSUser>>();

            var roles = new[] { "SuperAdmin", "Admin", "Editor", "Author", "Viewer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            }

            string email = "superadmin@admin.com";
            string password = "Test123!";
            string firstName = "Super";
            string lastName = "Admin";
            string displayName = "SuperAdminSheesh";
            DateTime birthDate = new DateTime(2000, 6, 13);

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new OrgPortal_CMSUser { UserName = email, Email = email,  FirstName = firstName, LastName = lastName, DisplayName = displayName, DateOfBirth = birthDate};
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "SuperAdmin");

            }
        }
    }
}
