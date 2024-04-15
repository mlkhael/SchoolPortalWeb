using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.DataAccess.Data;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using SchoolPortal.Utility;

namespace SchoolPortalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class AccountManagerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _db;

        public AccountManagerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }
        public IActionResult StudentList()
        {

            List<ApplicationUser> applicationUser = _unitOfWork.ApplicationUser.GetAll().OrderBy(u => u.Id).ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var a in applicationUser)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == a.Id).RoleId;
                a.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }

            applicationUser = applicationUser.Where(u => u.Role == "Student").ToList();

            return View(applicationUser);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
