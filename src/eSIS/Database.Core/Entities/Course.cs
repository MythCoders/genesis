using System.ComponentModel.DataAnnotations;

namespace eSIS.Database.Entities
{
    public class Course : BaseEntity
    {
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}