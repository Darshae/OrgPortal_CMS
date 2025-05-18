using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrgPortal_CMS.Data;
using OrgPortal_CMS.Models;

namespace OrgPortal_CMS.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AnnouncementController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
				var announcements = await _context.Announcements.ToListAsync();
				return View(announcements);
			}
            catch (Exception ex)
            {
				return BadRequest(ex.Message);
			}
        }

        public IActionResult CreateAnnouncementForm()
        {
            return View();
        }

		[HttpPost]
        public async Task<IActionResult> CreateAnnouncement(Announcement announcement)
        {
			try
			{
				await _context.Announcements.AddAsync(announcement);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

        [HttpPost]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                var announcementToDelete = await _context.Announcements.FindAsync(id);

                if (announcementToDelete == null) 
                {
                    return NotFound();
                }

                _context.Announcements.Remove(announcementToDelete);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        public async Task<IActionResult> EditAnnouncementForm(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return NotFound();
            return View(announcement); 
        }

        [HttpPost]
        public async Task<IActionResult> EditAnnouncement(Announcement announcement)
        {
            try
            {
                var announcementToEdit = await _context.Announcements.FindAsync(announcement.AnnouncementId);

                if(announcementToEdit == null)
                {
                    return NotFound();
                }

                announcementToEdit.AnnouncementId = announcement.AnnouncementId;
                announcementToEdit.Title = announcement.Title;
                announcementToEdit.Content = announcement.Content;
				announcementToEdit.Category = announcement.Category;
				announcementToEdit.Importance = announcement.Importance;
				announcementToEdit.LastUpdatedAt = DateTime.Now;
                announcementToEdit.AuthorId = announcement.AuthorId;
                announcementToEdit.IsPublished = announcement.IsPublished;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
