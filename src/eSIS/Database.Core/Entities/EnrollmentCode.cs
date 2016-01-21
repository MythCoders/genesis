using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Entities
{
    public class EnrollmentCode
    {
        [MaxLength(5)]
        [Index("IX_CEnrollmentCodeSystemCode", IsUnique = true)]
        public string SystemCode { get; set; }

        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsAdmission { get; set; }
        public bool IsWithdraw { get; set; }
        public bool IsActive { get; set; }
    }
}