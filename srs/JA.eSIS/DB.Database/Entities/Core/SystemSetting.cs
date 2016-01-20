using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JA.eSIS.DB.Database.Entities.Core
{
    [Table("SystemSetting", Schema = "core")]
    public partial class SystemSetting : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Key { get; set; }

        [StringLength(1000)]
        [Required(AllowEmptyStrings = true)]
        public string Value { get; set; }
    }
}