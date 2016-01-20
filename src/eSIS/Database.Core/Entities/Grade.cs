using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities
{
    /// <summary>
    /// Represents a 'Grade' that a student is in. 
    /// Ex: 9th Grade, 10th Grade, 11th Grade, etc.
    /// </summary>
    [Table("Grade", Schema = "sis")]
    public class Grade : BaseEntity
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public int PreviousGradeId { get; set; }
        public int NextGradeId { get; set; }
        public virtual Grade PreviousGrade { get; set; }
        public virtual Grade NextGrade { get; set; }
    }
}