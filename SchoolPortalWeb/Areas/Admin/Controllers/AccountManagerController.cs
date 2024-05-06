using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPortal.DataAccess.Data;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using SchoolPortal.Models.ViewModels;
using SchoolPortal.Utility;
using System.ComponentModel.Design;
using System.Reflection;

namespace SchoolPortalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class AccountManagerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;


        public AccountManagerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult StudentList()
        {
            List<ApplicationUser> applicationUser = _unitOfWork.ApplicationUser.ListApplicationUsers().Where(u => u.Role == "Student").ToList();

            return View(applicationUser);
        }

        public IActionResult AdminTeacherList()
        {
            List<ApplicationUser> applicationUser = _unitOfWork.ApplicationUser.ListApplicationUsers().Where(u => u.Role != "Student").ToList();

            return View(applicationUser);
        }


        public IActionResult Edit(string? id)
        {
            ApplicationUserVM vm = new ApplicationUserVM();

            vm.ApplicationUsers = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            vm.ApplicationUsers.Role = _unitOfWork.GetUserRole(vm.ApplicationUsers.Id);


            //Create SelectLists

            List<string> BranchOfServices = new();
            List<string> Ranks = new();

            FieldInfo[] allStaticDetails = typeof(StaticDetails).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var u in allStaticDetails)
            {
                if (u.Name.StartsWith("BranchOfService_") && u.FieldType == typeof(string))
                {
                    BranchOfServices.Add((string)u.GetValue(null));
                }
                if (u.Name.StartsWith("Rank_") && u.FieldType == typeof(string))
                {
                    Ranks.Add((string)u.GetValue(null));
                }


            }

            vm.RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            }).ToList();

            vm.BranchOfServiceList = BranchOfServices.Select(i => new SelectListItem
            {
                Text = i,
                Value = i,
            }).ToList();

            vm.RankList = Ranks.Select(i => new SelectListItem
            {
                Text = i,
                Value = i,
            }).ToList();


            //return

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationUserVM vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _unitOfWork.ApplicationUser.Get(u => u.Id == vm.ApplicationUsers.Id);

                user.FirstName = vm.ApplicationUsers.FirstName;
                user.LastName = vm.ApplicationUsers.LastName;
                user.UserName = vm.ApplicationUsers.UserName;
                user.YearEnrolled = vm.ApplicationUsers.YearEnrolled;
                user.BranchOfService = vm.ApplicationUsers.BranchOfService;
                user.Rank = vm.ApplicationUsers.Rank;

                /* _unitOfWork.ApplicationUser.Update(user);

                 _unitOfWork.Save();*/

                var result = _userManager.UpdateAsync(user);

                if (result.Result.Succeeded)
                { 
                    return RedirectToAction("StudentList");
                }

                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(vm);
        }


        [HttpPost]
        public IActionResult DeleteUser(string? id)
        {
            string activeUser = _unitOfWork.GetUserId(User);

            if (activeUser == id)
            {
                return Json("WARNING: This user is currently logged in, unable to delete");
            }

            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == id);

            if (applicationUser != null)
            {
                _unitOfWork.ApplicationUser.Remove(applicationUser);
                _unitOfWork.Save();
                return Json("User Deleted Successfully");
            }

            //error
            return Json("Error deleting user.");

        }
    }
}

