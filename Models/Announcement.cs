namespace OrgPortal_CMS.Models
{
    public enum Importance
    {
        High,
        Medium,
        Low
    }
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = false;
        public int AuthorId { get; set; }
        public string Category{ get; set; } = string.Empty ;
        public Importance Importance { get; set; }
    }
}
