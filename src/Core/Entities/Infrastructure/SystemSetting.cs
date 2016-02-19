using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MC.eSIS.Core.Entities.Infrastructure
{
    [Table("SystemSetting", Schema = "inf")]
    public class SystemSetting : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Key { get; set; }

        [StringLength(1000)]
        [Required(AllowEmptyStrings = true)]
        public string Value { get; set; }
    }
}