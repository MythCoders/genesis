using System.ComponentModel.DataAnnotations;

namespace MC.eSIS.Core.Security
{
    public class ConfirmAccountData
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "#1 Question")]
        public int? ResetQuestion1ID { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Reset Question Answer must be between 1 and 200 characters")]
        [Display(Name = "#1 Answer")]
        public string ResetQuestion1Answer { get; set; }

        [Required]
        [Display(Name = "#2 Question")]
        public int? ResetQuestion2ID { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Reset Question Answer must be between 1 and 200 characters")]
        [Display(Name = "#2 Answer")]
        public string ResetQuestion2Answer { get; set; }
    }
}