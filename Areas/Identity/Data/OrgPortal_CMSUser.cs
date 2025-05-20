using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OrgPortal_CMS.Areas.Identity.Data;
    
// Add profile data for application users by adding properties to the OrgPortal_CMSUser class
public class OrgPortal_CMSUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName{ get; set; }
    public string DisplayName { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime DateOfBirth{ get; set; }

}

