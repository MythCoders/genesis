using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eSIS.Core.Security
{
    public enum AccountStatusEnum
    {
        [Description("Unvalidated")]
        Unvalidated = 0,

        [Description("Validated")]
        Validated = 5
    }

    public class AuthenticationData
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
