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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }

        public List<ApplicationUser> ListApplicationUsers()
        {
            List<ApplicationUser> applicationUser = GetAll().ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var a in applicationUser)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == a.Id).RoleId;
                a.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }

            // applicationUser = applicationUser.Where(u => u.Role == "Student").OrderBy(u => u.Id).ToList();

            return applicationUser;
        }
    }
}
