using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities
{
    /// <summary>
    /// All configurations are based on a single year. This allows for historical recording keeping
    /// </summary>
    [Table("SchoolYear", Schema = "sis")]
    public class SchoolYear : BaseEntity
    {
        [Index("IX_SchoolIdYear", 1, IsUnique = true)]
        public int SchoolId { get; set; }

        [Index("IX_SchoolIdYear", 2, IsUnique = true)]
        public int Year { get; set; }

        public virtual School School { get; set; }
    }
}