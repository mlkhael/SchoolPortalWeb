using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAdminAnnouncementRepository AdminAnnouncement { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
