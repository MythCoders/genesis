using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities
{
    [Table("EnrollmentCode", Schema = "sis")]
    public class EnrollmentCode : BaseEntity
    {
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsAdmission { get; set; }
        public bool IsWithdraw { get; set; }
        public bool IsActive { get; set; }
    }
}