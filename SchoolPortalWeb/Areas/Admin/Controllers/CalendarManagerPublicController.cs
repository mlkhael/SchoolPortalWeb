using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;

namespace SchoolPortalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CalendarManagerPublicController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CalendarManagerPublicController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            IEnumerable<CalendarManager> events = _unitOfWork.CalendarManager.GetAll();

            return Json(events);
        }


    }
}
