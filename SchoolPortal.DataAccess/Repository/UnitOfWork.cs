using Microsoft.AspNetCore.Mvc;
using SchoolPortal.DataAccess.Data;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IAdminAnnouncementFilesRepository AdminAnnouncementFiles { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICalendarManagerRepository CalendarManager { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AdminAnnouncement = new AdminAnnouncementRepository(_db);
            AdminAnnouncementFiles = new AdminAnnouncementFilesRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            CalendarManager = new CalendarManagerRepository(_db);
        }

        public void Save() 
        {
            _db.SaveChanges();
        }

        public string GetUserId(ClaimsPrincipal User)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return userId;
        }

        public string GetUserRole(string id) 
        {
            var userRoles = _db.UserRoles.ToList(); //list all user roles (role Id and user Id)
            var roles = _db.Roles.ToList(); //list all actual roles (role name and role Id)

            var roleId = userRoles.FirstOrDefault(u => u.UserId == id).RoleId; //find role id for input id/user id
            var role = roles.FirstOrDefault(u => u.Id == roleId).Name; //find role name for role id

            return role;
        }
    }
}
