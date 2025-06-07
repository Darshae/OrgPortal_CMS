using OrgPortal_CMS.Areas.Identity.Data;
using OrgPortal_CMS.Models;

namespace OrgPortal_CMS.ViewModel
{
	public class AnnouncementViewModel
	{
		public int AnnouncementId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime AnnouncementStartDateTime { get; set; }
		public DateTime AnnouncementEndDateTime { get; set; }
		public DateTime PostDateTime { get; set; }
		public string Category { get; set; } = string.Empty;
		public string Excerpt { get; set; } = string.Empty;
		public Importance Importance { get; set; }

		public IFormFile? PictureFile { get; set; }
	}
}
