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
    public class AdminAnnouncementFilesRepository : Repository<AdminAnnouncementFiles>, IAdminAnnouncementFilesRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminAnnouncementFilesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AdminAnnouncementFiles obj)
        {
            _db.AdminAnnouncementFiles.Update(obj);
        }
    }
}
