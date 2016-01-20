using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities
{
    [Table("School", Schema = "sis")]
    public class School : BaseEntity
    {
        public int AddressId { get; set; }
        public int DistrictId { get; set; }

        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(3)]
        public string ShortCode { get; set; }

        public virtual Address Address { get; set; }
        public virtual District District { get; set; }
    }
}