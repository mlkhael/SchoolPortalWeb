using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using SchoolPortal.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace SchoolPortalWeb.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
           
        }

        public IActionResult Index()
        {
            
            AdminAnnouncementVM vm = new AdminAnnouncementVM();
            vm.AdminAnnouncement = new AdminAnnouncement();

            if (User.Identity.IsAuthenticated)
            {
                //vm.AdminAnnouncement.ApplicationUserId = GetUserId();
                vm.AdminAnnouncement.ApplicationUserId = _unitOfWork.GetUserId(User);
            }
            vm.Posts = _unitOfWork.AdminAnnouncement.GetAll(includeProperties: "ApplicationUser, AdminAnnouncementFiles")
                .OrderByDescending(u => u.DatePosted).ToList();

            return View(vm);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*public string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return userId;
        }*/
    }
}
