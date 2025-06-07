using OrgPortal_CMS.Areas.Identity.Data;

namespace OrgPortal_CMS.Models
{
    public enum Importance
    {
        Critical,
        High,
        Medium,
        Low
    }

    public enum Status
    {
        Draft,
        Review,
        Approved,
        Pending,
        Upcoming,
        Published,
        Revision,
        Denied
    }

    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } = DateTime.Now;
        public DateTime AnnouncementStartDateTime{ get; set; }
        public DateTime AnnouncementEndDateTime { get; set; }
        public DateTime PostDateTime { get; set; }
        public byte[]? Picture { get; set; }
        public bool IsPublished { get; set; } = false;
        public string Category{ get; set; } = string.Empty ;
        public string Excerpt { get; set; } = string.Empty;
        public Importance Importance { get; set; }
        public Status Status { get; set; } = Status.Draft;
        public string AuthorId { get; set; }
        public OrgPortal_CMSUser Author { get; set; }   
    }
}
