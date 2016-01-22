using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Entities.Core
{
    [Table("Announcement", Schema = "core")]
    public partial class Announcement : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Text")]
        [UIHint("TextArea")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [UIHint("DateTime")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [UIHint("DateTime")]
        public DateTime? EndDate { get; set; }
    }
}