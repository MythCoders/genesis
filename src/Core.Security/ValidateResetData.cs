using System.ComponentModel.DataAnnotations;

namespace MC.eSIS.Core.Security
{
    public class ValidateResetData
    {
        [Required]
        public string Token { get; set; }

        public AccountStatusEnum AccountStatus { get; set; }

        [Required]
        public int ResetQuestion1ID { get; set; }

        public string ResetQuestion1Question { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Reset Question Answer must be between 1 and 200 characters")]
        [Display(Name = "#1 Answer")]
        public string ResetQuestion1Answer { get; set; }

        [Required]
        public int ResetQuestion2ID { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = "Reset Question Answer must be between 1 and 200 characters")]
        public string ResetQuestion2Question { get; set; }

        [Required]
        [Display(Name = "#2 Answer")]
        public string ResetQuestion2Answer { get; set; }
    }
}