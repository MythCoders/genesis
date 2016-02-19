using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MC.eSIS.Core.Entities
{
    [Table("School", Schema = "sis")]
    public class School : BaseEntity
    {
        [MaxLength(45)]
        public string Name { get; set; }

        public int AddressId { get; set; }

        public int DistrictId { get; set; }

        public int SchoolTypeId { get; set; }

        public virtual SchoolType SchoolType { get; set; }
        public virtual Address Address { get; set; }
        public virtual District District { get; set; }
    }
}