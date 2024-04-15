using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Models.ViewModels
{
    public class AccountManagerVM
    {
        [ValidateNever]
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
