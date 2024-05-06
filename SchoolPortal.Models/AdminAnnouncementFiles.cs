using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Models
{
    public class AdminAnnouncementFiles
    {
        public int Id { get; set; }
        [Required]
        public string FileUrl { get; set; }
        public int AdminAnnouncementId { get; set; }
        [ForeignKey("AdminAnnouncementId")]
        public AdminAnnouncement AdminAnnouncement { get; set; }
    }
}
