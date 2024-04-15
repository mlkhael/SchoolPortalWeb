using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using SchoolPortal.Models.ViewModels;
using SchoolPortal.Utility;
using System.Security.Claims;

namespace SchoolPortalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Teacher)]
    public class AdminAnnouncementController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminAnnouncementController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            AdminAnnouncementVM vm = new AdminAnnouncementVM();
            vm.AdminAnnouncement = new AdminAnnouncement();

            vm.AdminAnnouncement.ApplicationUserId = GetUserId();
            vm.Posts = _unitOfWork.AdminAnnouncement.GetAll(includeProperties: "ApplicationUser")
                .OrderByDescending(u => u.DatePosted).ToList();

            return View(vm);
        }

        [HttpPost] //Create Post
        public IActionResult Index(AdminAnnouncement adminAnnouncement)
        {
            if (ModelState.IsValid)
            {
                adminAnnouncement.DatePosted = DateTime.Now;

                _unitOfWork.AdminAnnouncement.Add(adminAnnouncement);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                //error
            }

            AdminAnnouncement adminAnnoucement = _unitOfWork.AdminAnnouncement.Get(u => u.Id == id);

            if (adminAnnoucement == null) 
            { 
                //error
            }

            return View(adminAnnoucement);
        }

        [HttpPost]
        public IActionResult Edit(AdminAnnouncement adminAnnouncement) 
        {
            

            if (ModelState.IsValid)
            {
                adminAnnouncement.IsEditted = true;
                adminAnnouncement.DateEditted = DateTime.Now;

                _unitOfWork.AdminAnnouncement.Update(adminAnnouncement);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(adminAnnouncement.Id);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var adminAnnouncement = _unitOfWork.AdminAnnouncement.Get(u => u.Id == id);

            if (adminAnnouncement != null)
            {
                _unitOfWork.AdminAnnouncement.Remove(adminAnnouncement);
                _unitOfWork.Save();
                return Json("Announcement Deleted Successfully");
            }

            //error
            return Json("Error deleting announcement.");

        }

        public string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return userId;
        }


    }
}
