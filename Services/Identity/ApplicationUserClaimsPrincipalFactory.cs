using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrgPortal_CMS.Areas.Identity.Data;

namespace OrgPortal_CMS.Services.Identity
{
	public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<OrgPortal_CMSUser, IdentityRole>
	{
		public ApplicationUserClaimsPrincipalFactory(UserManager<OrgPortal_CMSUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionsAccesor) : base(userManager, roleManager, optionsAccesor)
		{
		}

		protected override async Task<ClaimsIdentity> GenerateClaimsAsync(OrgPortal_CMSUser user)
		{
			var identity = await base.GenerateClaimsAsync(user);
			identity.AddClaim(new Claim("DisplayName", user.DisplayName ?? ""));
			return identity;
		}
	}
}
