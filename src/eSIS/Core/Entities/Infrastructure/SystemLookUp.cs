using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities.Infrastructure
{
    [Table("SystemLookUp", Schema = "inf")]
    public class SystemLookUp : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string LookUpKey { get; set; }

        [Required]
        [StringLength(1000)]
        public string Value { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}