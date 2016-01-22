using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities.SIS
{
    /// <summary>
    /// A School District is a grouping of schools. With schools being grouped into districts, this allows for an entire state to adopt eSIS! 
    /// </summary>
    [Table("District", Schema = "sis")]
    public class District : BaseEntity
    {
        [MaxLength(45)]
        public string Name { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual List<School> Schools { get; set; } 
    }
}