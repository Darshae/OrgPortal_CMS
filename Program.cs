using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrgPortal_CMS.Areas.Identity.Data;
using OrgPortal_CMS.Data;
using OrgPortal_CMS.Services;
using OrgPortal_CMS.Services.Identity;
using System.Threading.Tasks;

namespace OrgPortal_CMS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

            builder.Services.AddDefaultIdentity<OrgPortal_CMSUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>() 
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IUserClaimsPrincipalFactory<OrgPortal_CMSUser>, ApplicationUserClaimsPrincipalFactory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                await SeedData.InitializeAsync(scope.ServiceProvider);
                Console.WriteLine("I worked");
            }

            app.Run();
        }
    }
}
