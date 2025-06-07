using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrgPortal_CMS.Data;
using OrgPortal_CMS.Models;
using OrgPortal_CMS.ViewModel;

namespace OrgPortal_CMS.Controllers
{
    [Authorize]

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
                var announcements = await _context.Announcements.Include(a => a.Author).Where(a => a.IsPublished == true).ToListAsync();

                if (announcements.Count == 0) 
                {
                    return NotFound();
                }

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
        public async Task<IActionResult> CreateAnnouncement(AnnouncementViewModel announcementViewModel)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    return Unauthorized();
                }

                var announcement = new Announcement
                {
                    Title = announcementViewModel.Title,
                    Category = announcementViewModel.Category,
                    Content = announcementViewModel.Content,
                    Excerpt = announcementViewModel.Excerpt,
                    Importance = announcementViewModel.Importance,
                    PostDateTime = announcementViewModel.PostDateTime,
                    AnnouncementStartDateTime = announcementViewModel.AnnouncementStartDateTime,
                    AnnouncementEndDateTime = announcementViewModel.AnnouncementEndDateTime,

					AuthorId = userId,
					CreatedAt = DateTime.Now,
					LastUpdatedAt = DateTime.Now
				};

                if(announcementViewModel.PictureFile != null && announcementViewModel.PictureFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await announcementViewModel.PictureFile.CopyToAsync(ms);
                    announcement.Picture= ms.ToArray();
                }

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

                if (announcementToEdit == null)
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

        public async Task<IActionResult> GetAllAnnouncements()
        {
            try
            {
                var role = User.FindFirstValue(ClaimTypes.Role);

                if (role == "SuperAdmin")
                {
                    var announcements = await _context.Announcements.Include(a => a.Author).Where(a => a.IsPublished == true).ToListAsync();
                    return View(announcements);
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var announcements = await _context.Announcements.Include(a => a.Author).Where(a => a.AuthorId == userId).ToListAsync();
                    return View(announcements);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Draft Section
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> GetAllDrafts()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var drafts = await _context.Announcements.Where(a => a.AuthorId == userId && a.Status == Status.Draft).ToListAsync();
                return View(drafts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> SubmitDraft(int id)
        {
            try
            {
                var draftToSubmit = await _context.Announcements.FindAsync(id);

                if(draftToSubmit == null)
                {
                    return NotFound();
                }

                draftToSubmit.Status = Status.Review;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAllDrafts));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Review Section
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _context.Announcements.Where(a => a.Status == Status.Review).ToListAsync();

                if(reviews == null)
                {
                    return NotFound();
                }

                return View(reviews);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ViewReview(int id)
        {
            try
            {
                var review = await _context.Announcements.FindAsync(id);

                if(review == null)
                {
                    return NotFound();
                }

                return View(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ApproveReview(int id)
        {
            try
            {
                var announcement = await _context.Announcements.FindAsync(id);

                if(announcement == null)
                {
                    return NotFound();
                }

                announcement.Status = Status.Upcoming;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAllReviews));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ReviseReview(int id)
        {
            try
            {
                var announcement = await _context.Announcements.FindAsync(id);

                if (announcement == null)
                {
                    return NotFound();
                }

                announcement.Status = Status.Revision;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAllReviews));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DenyReview(int id)
        {
            try
            {
                var announcement = await _context.Announcements.FindAsync(id);

                if (announcement == null)
                {
                    return NotFound();
                }

                announcement.Status = Status.Denied;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAllReviews));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Revisions Section
        public async Task<IActionResult> GetAllRevisions()
        {
            try
            {
                var revisions = await _context.Announcements.Where(a => a.Status == Status.Revision).ToListAsync();

                if(revisions == null)
                {
                    return NotFound();
                }

                return View(revisions);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> SubmitRevised(int id)
        {
            try
            {
                var revisedAnnouncement = await _context.Announcements.FindAsync(id);

                if (revisedAnnouncement == null)
                {
                    return NotFound();
                }

                revisedAnnouncement.Status = Status.Review;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAllDrafts));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Upcoming Section
        public async Task<IActionResult> GetAllUpcoming()
        {
            try
            {
                var upcoming = await _context.Announcements.Where(a => a.Status == Status.Upcoming).ToListAsync();

                if(upcoming == null)
                {
                    return NotFound();
                }

                return View(upcoming);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> PublishAnnouncementNow(int id)
        {
            try
            {
                var announcement = await _context.Announcements.FindAsync(id);
                if (announcement == null) 
                {
                    return NotFound();
                }

                announcement.Status = Status.Published;
                announcement.IsPublished = true;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAllUpcoming));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Published section
        public async Task<IActionResult> GetAllPublished()
        {
            try
            {
                var announcements = await _context.Announcements.Where(a => a.Status == Status.Published && a.IsPublished == true).ToListAsync();

                if(announcements == null)
                {
                    return NotFound();
                }

                return View(announcements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Publish and Unpublish Controllers
        [HttpPost]
        public async Task<IActionResult> PublishAnnouncement(int id)
        {
            try
            {
                var announcement = await _context.Announcements.FindAsync(id);

                if (announcement == null)
                {
                    return NotFound();
                }

                announcement.IsPublished = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAllAnnouncements));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UnpublishAnnouncement(int id)
        {
            try
            {
                var announcement = await _context.Announcements.FindAsync(id);

                if (announcement == null)
                {
                    return NotFound();
                }

                announcement.IsPublished = false;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAllAnnouncements));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
