using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
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

        public IActionResult IndexCreate()
        {
            AdminAnnouncementVM vm = new AdminAnnouncementVM();
            vm.AdminAnnouncement = new AdminAnnouncement();

            vm.AdminAnnouncement.ApplicationUserId = _unitOfWork.GetUserId(User);
            vm.Posts = _unitOfWork.AdminAnnouncement.GetAll(includeProperties: "ApplicationUser , AdminAnnouncementFiles")
                .OrderByDescending(u => u.DatePosted).ToList();

            return View(vm);
        }

        [HttpPost] //Create Post
        public IActionResult IndexCreate(AdminAnnouncement adminAnnouncement, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                adminAnnouncement.DatePosted = DateTime.Now;

                _unitOfWork.AdminAnnouncement.Add(adminAnnouncement);
                _unitOfWork.Save();

                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = file.FileName.Replace(" ", "_");
                        string announcementPath = @"files\announcements\announcement-" + adminAnnouncement.Id;
                        string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, announcementPath);

                        if (!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        AdminAnnouncementFiles adminAnnouncementfiles = new AdminAnnouncementFiles()
                        {
                            FileUrl = @"\" + announcementPath + @"\" +fileName,
                            AdminAnnouncementId = adminAnnouncement.Id,
                        };

                        if (adminAnnouncement.AdminAnnouncementFiles == null)
                            adminAnnouncement.AdminAnnouncementFiles = new List<AdminAnnouncementFiles>();

                        adminAnnouncement.AdminAnnouncementFiles.Add(adminAnnouncementfiles);
                    }    

                   _unitOfWork.AdminAnnouncement.Update(adminAnnouncement);
                   _unitOfWork.Save();
                }
            }

            return RedirectToAction("IndexCreate");
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

                return RedirectToAction("IndexCreate");
            }
            return View(adminAnnouncement.Id);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var adminAnnouncement = _unitOfWork.AdminAnnouncement.Get(u => u.Id == id);

            if (adminAnnouncement != null)
            {
                string announcementPath = @"files\announcements\announcement-" + adminAnnouncement.Id;
                string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, announcementPath);

                if (Directory.Exists(finalPath)) {
                    string[] targetPaths = Directory.GetFiles(finalPath);

                    foreach (string targetPath in targetPaths) 
                    {
                        System.IO.File.Delete(targetPath);
                    }

                    Directory.Delete(finalPath);
                
                }

                _unitOfWork.AdminAnnouncement.Remove(adminAnnouncement);
                _unitOfWork.Save();

                return Json("Announcement Deleted Successfully");
            }

            //error
            return Json("Error deleting announcement.");

        }
/*
        [HttpGet]
        public IActionResult DownloadFile(string filePath)
        {

            if (!System.IO.File.Exists(filePath))
                return NotFound(); // or some appropriate error handling

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", Path.GetFileName(filePath));
        }
*/
    }
}
