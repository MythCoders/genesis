using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Entities
{
    public class Course
    {
        [MaxLength(5)]
        [Index("IX_CourseSystemCode", IsUnique = true)]
        public string SystemCode { get; set; }

        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}