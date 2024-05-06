using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using SchoolPortal.Models.ViewModels;
using SchoolPortal.Utility;
using System.Security.Claims;

namespace SchoolPortalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Teacher)]
    public class CalendarManagerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CalendarManagerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            //_unitOfWork.CalendarManager.GetAll().Where(x => x.StartDate.Month == DateTime.Now.Month);

            List<CalendarManager> list = _unitOfWork.CalendarManager.GetAll().OrderByDescending(x => x.StartDate).ToList();
            return View(list);
        }
        public IActionResult CreateEdit(int? id)
        {

           
            CalendarManager calendarManager = new CalendarManager();

            if (id != null || id == 0)
            {
                calendarManager = _unitOfWork.CalendarManager.Get(u => u.Id == id);
            }

            else
            {
                calendarManager.StartDate = DateTime.Today.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                calendarManager.EndDate = DateTime.Today.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            }

            return View(calendarManager);
        }
        [HttpPost]
        public IActionResult CreateEdit(CalendarManager calendarManager)
        {
            if (ModelState.IsValid)
            {
                if (calendarManager.Id == null || calendarManager.Id == 0)
                {
                    calendarManager.isAllDay = (calendarManager.StartDate == calendarManager.EndDate);
                    _unitOfWork.CalendarManager.Add(calendarManager);
                }

                else
                {
                    _unitOfWork.CalendarManager.Update(calendarManager);
                }

                _unitOfWork.Save();
            }

            else
            {
                //error
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteEvent(int? id)
        {
            var calendarManager = _unitOfWork.CalendarManager.Get(u => u.Id == id);

            if (calendarManager != null)
            {
                _unitOfWork.CalendarManager.Remove(calendarManager);
                _unitOfWork.Save();
                return Json("Event Deleted Successfully");
            }

            //error
            return Json("Error deleting event.");

        }

        public IActionResult Calendar()
        {
            return View();
        }
    }
}
