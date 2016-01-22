using System.ComponentModel.DataAnnotations;

namespace eSIS.Core.Entities.SIS
{
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