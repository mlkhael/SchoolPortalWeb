using SchoolPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAdminAnnouncementRepository AdminAnnouncement { get; }
        IAdminAnnouncementFilesRepository AdminAnnouncementFiles { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ICalendarManagerRepository CalendarManager { get; }
        void Save();

        string GetUserId(ClaimsPrincipal User);

        string GetUserRole(string id);
    }
}
