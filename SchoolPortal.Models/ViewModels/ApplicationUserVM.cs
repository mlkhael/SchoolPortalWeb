using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolPortal.Models.ViewModels
{
    public class ApplicationUserVM
    {
        public ApplicationUser ApplicationUsers { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BranchOfServiceList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RankList { get; set; }
    }
}
