using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Entities
{
    /// <summary>
    /// Represents a 'Grade' that a student is in. 
    /// Ex: 9th Grade, 10th Grade, 11th Grade, etc.
    /// </summary>
    [Table("Grade", Schema = "sis")]
    public class Grade : BaseEntity
    {
        [MaxLength(5)]
        [Index("IX_GradeSystemCode", IsUnique = true)]
        public string SystemCode { get; set; }

        [MaxLength(45)]
        public string Name { get; set; }

        public int? PreviousGradeId { get; set; }
        
        public int? NextGradeId { get; set; }

        public virtual Grade PreviousGrade { get; set; }

        public virtual Grade NextGrade { get; set; }
    }
}