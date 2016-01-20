using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities
{
    /// <summary>
    /// A School District is a grouping of schools. With schools being grouped into districts, this allows for an entire state to adopt eSIS! 
    /// </summary>
    [Table("District", Schema = "sis")]
    public class District : BaseEntity
    {
        public int AddressId { get; set; }

        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(3)]
        public string ShortCode { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<School> Schools { get; set; } 
    }
}