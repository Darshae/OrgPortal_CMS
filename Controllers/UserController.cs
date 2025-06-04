using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrgPortal_CMS.Areas.Identity.Data;
using OrgPortal_CMS.ViewModel;
using System.Threading.Tasks;

namespace OrgPortal_CMS.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<OrgPortal_CMSUser> _userManager;
        public UserController(UserManager<OrgPortal_CMSUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) { 
                return NotFound();
            }

            var model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.DisplayName,
                DateOfBirth = user.DateOfBirth,
            };

            return View(model);
        }
    }
}
