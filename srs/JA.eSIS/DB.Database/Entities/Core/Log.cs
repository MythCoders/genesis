using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities.Core
{
    [Table("Log", Schema = "core")]
    public partial class Log : BaseEntity
    {
        [Display(Name = "Message")]
        public string Message { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Stacktrace")]
        public string StackTrace { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Source")]
        public string Source { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }

        [Required]
        [Display(Name = "Is Error?")]
        public bool IsError { get; set; }
    }
}