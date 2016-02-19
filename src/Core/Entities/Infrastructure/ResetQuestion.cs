using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MC.eSIS.Core.Entities.Infrastructure
{
    [Table("ResetQuestion", Schema = "inf")]
    public class ResetQuestion : BaseEntity
    {
        [Required]
        [Display(Name = "Question Text")]
        [StringLength(2000)]
        [UIHint("TextArea")]
        public string QuestionText { get; set; }

        [Required]
        [UIHint("Switch")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}