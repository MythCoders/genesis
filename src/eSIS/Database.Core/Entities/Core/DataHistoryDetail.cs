using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities.Core
{
    [Table("DataHistoryDetail", Schema = "core")]
    public partial class DataHistoryDetail : BaseEntity
    {
        /// <summary>
        /// When foreign keys are named properly, it allows
        /// Entity Framework to detect the relationship automatically.
        /// Please note, the parent table MUST
        /// </summary>
        /// <warning>
        /// The parent table MUST have a virtual ICollection property
        /// of the child table declared.
        /// </warning>
        [Required]
        public int DataHistoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string FieldName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string BeforeValue { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        public string AfterValue { get; set; }
    }
}
