using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int YearEnrolled { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [Required]
        public string BranchOfService { get; set; }
        [Required]
        public string Rank { get; set; }

    }
}
