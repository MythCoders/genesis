using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities.Infrastructure
{
    [Table("Log", Schema = "inf")]
    public class Log : BaseEntity
    {
        [StringLength(60)]
        public string UserIdentifer { get; set; }

        [StringLength(250)]
        public string UserAgentString { get; set; }

        [StringLength(20)]
        public string IpAddress { get; set; }

        [StringLength(250)]
        public string Message { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Stacktrace")]
        public string StackTrace { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Source")]
        public string Source { get; set; }

        [Required]
        [Display(Name = "Is Error?")]
        public bool IsError { get; set; }
    }
}