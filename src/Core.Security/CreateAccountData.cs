using System.ComponentModel.DataAnnotations;

namespace MC.eSIS.Core.Security
{
    public class CreateAccountData
    {
        [EmailAddress]
        [Required]
        [StringLength(50)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        [Display(Name = "Please enter the text from the image below")]
        public string Answer { get; set; }

        [Display(Name = "CAPTCHA Check")]
        public byte[] Check { get; set; }
    }
}