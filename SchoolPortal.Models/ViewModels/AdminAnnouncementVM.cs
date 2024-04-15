using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Models.ViewModels
{
    public class AdminAnnouncementVM
    {
        public AdminAnnouncement AdminAnnouncement { get; set; }
        [ValidateNever]
        public List<AdminAnnouncement> Posts { get; set; }
    }
}
