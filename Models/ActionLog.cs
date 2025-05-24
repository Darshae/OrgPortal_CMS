using OrgPortal_CMS.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace OrgPortal_CMS.Models
{
    public class ActionLog
    {
        [Key]
        public int ActionId { get; set; }
        public int AnnouncementId{ get; set; }
        public Announcement Announcement { get; set; }
        public string ChangedByUserId { get; set; }
        public OrgPortal_CMSUser User { get; set; }
        public DateTime ChangeTimestamp{ get; set; }
        public Status OldStatus { get; set; }
        public Status NewStatus { get; set; }
        public string ChangeDescription { get; set; } = string.Empty;
    }
}
