using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Models
{
    public class AdminAnnouncement
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title cannot be blank.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content cannot be blank.")]
        public string Description { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        [Required]
        public bool IsEditted { get; set; } = false;
        [Required]
        public DateTime DateEditted { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
