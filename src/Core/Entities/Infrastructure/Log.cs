using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities.Infrastructure
{
    [Table("Log", Schema = "inf")]
    public class Log : BaseEntity
    {
        [StringLength(200)]
        public string MachineName { get; set; }

        [Required]
        [StringLength(200)]
        public string SiteName { get; set; }

        [Required]
        public DateTime Logged { get; set; }

        [Required]
        [StringLength(5)]
        public string Level { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(Int32.MaxValue)]
        public string Message { get; set; }

        [StringLength(300)]
        public string Logger { get; set; }

        [StringLength(Int32.MaxValue)]
        public string Properties { get; set; }

        [StringLength(200)]
        public string ServerName { get; set; }

        [StringLength(100)]
        public string Port { get; set; }

        [StringLength(2000)]
        public string Url { get; set; }

        public bool? Https { get; set; }

        [StringLength(100)]
        public string ServerAddress { get; set; }

        [StringLength(100)]
        public string RemoteAddress { get; set; }
        
        [StringLength(300)]
        public string Callstie { get; set; }

        [StringLength(Int32.MaxValue)]
        public string Exception { get; set; }
    }
}