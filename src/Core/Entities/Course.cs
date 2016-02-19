using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MC.eSIS.Core.Entities
{
    [Table("Course", Schema = "sis")]
    public class Course : BaseEntity
    {
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}