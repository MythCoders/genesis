using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities.Core
{
    [Table("LookUp", Schema = "core")]
    public partial class LookUp : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Group { get; set; }

        [Required]
        [StringLength(1000)]
        public string Value { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}