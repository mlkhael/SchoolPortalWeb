using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SchoolPortal.DataAccess.Data;
using SchoolPortal.DataAccess.Migrations;
using SchoolPortal.Models;
using SchoolPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            //add migrations if not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }

            catch (Exception ex) { }

            //add roles if not applied
            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Student).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Student)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Teacher)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();

                //create admin if not exists
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@studentportal.com",
                    Email = "admin@studentportal.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    BranchOfService = "PAF",
                    Rank = "Colonel (COL)",
                    YearEnrolled = 1000
                }, "Password_123").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@studentportal.com");
                _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();

                //create teacher
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "teacher001",
                    Email = "teacher@studentportal.com",
                    FirstName = "Test",
                    LastName = "Teacher",
                    BranchOfService = "PAF",
                    Rank = "Colonel (COL)",
                    YearEnrolled = 1000
                }, "Password_123").GetAwaiter().GetResult();

                 user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "teacher@studentportal.com");
                _userManager.AddToRoleAsync(user, StaticDetails.Role_Teacher).GetAwaiter().GetResult();

                //create student
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "O-23465",
                    Email = "student@studentportal.com",
                    FirstName = "Test",
                    LastName = "Student",
                    BranchOfService = "PAF",
                    Rank = "1st Lieutenant (1LT)",
                    YearEnrolled = 1000
                }, "Password_123").GetAwaiter().GetResult();

                user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student@studentportal.com");
                _userManager.AddToRoleAsync(user, StaticDetails.Role_Student).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
