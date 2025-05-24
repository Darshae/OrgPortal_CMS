using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrgPortal_CMS.Areas.Identity.Data;
using OrgPortal_CMS.Models;

namespace OrgPortal_CMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<OrgPortal_CMSUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ActionLog> ActionsLogs { get; set; }
    }
}
