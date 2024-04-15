using SchoolPortal.DataAccess.Data;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.DataAccess.Repository
{
    public class AdminAnnouncementRepository : Repository<AdminAnnouncement>, IAdminAnnouncementRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminAnnouncementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AdminAnnouncement obj)
        {
            _db.AdminAnnouncements.Update(obj);
        }

    }
}
