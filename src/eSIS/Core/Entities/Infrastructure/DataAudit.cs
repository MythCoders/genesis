using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities.Infrastructure
{
    [Table("DataAudit", Schema = "inf")]
    public class DataAudit : BaseEntity
    {
        [Required]
        public string Action { get; set; }

        [Required]
        public string Table { get; set; }

        [Required]
        public int RecordId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserIpAddress { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// This declaration, along with the declaration of a primary key columns,
        /// allows Entity Framework to automatically detect the relationship and create
        /// the necessary database constraints
        /// </summary>
        /// <remarks>
        /// This property is virtual to allow Lazy Loading with Entity Framework. Meaning, the database
        /// context will control when to go back to the database and get more information when it's requested.
        /// This will allow for smaller, more frequent trips to the database rather than large, less frequent trips
        /// where we might not need all of the information that was returned
        /// </remarks>
        public virtual ICollection<DataAuditDetail> Details { get; set; }
    }
}
