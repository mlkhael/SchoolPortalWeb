using Microsoft.AspNetCore.Mvc;
using SchoolPortal.DataAccess.Data;
using SchoolPortal.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IAdminAnnouncementRepository AdminAnnouncement { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AdminAnnouncement = new AdminAnnouncementRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public void Save() 
        {
            _db.SaveChanges();
        }
    }
}
