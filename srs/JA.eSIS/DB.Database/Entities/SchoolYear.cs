using System.ComponentModel.DataAnnotations.Schema;

namespace JA.eSIS.DB.Database.Entities
{
    /// <summary>
    /// All configurations are based on a single year. This allows for historical recording keeping
    /// </summary>
    [Table("SchoolYear", Schema = "sis")]
    public class SchoolYear : BaseEntity
    {
        public int SchoolId { get; set; }
        public int Year { get; set; }
        public virtual School School { get; set; }
    }
}