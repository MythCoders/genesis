using System.ComponentModel.DataAnnotations;

namespace MC.eSIS.Core.Security
{
    public class ResetPasswordData
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}