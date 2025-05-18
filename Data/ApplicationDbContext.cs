using Microsoft.EntityFrameworkCore;
using OrgPortal_CMS.Models;

namespace OrgPortal_CMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { }
        

        public DbSet<Announcement> Announcements { get; set; }

    }
}
