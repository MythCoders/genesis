using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities
{
    /// <summary>
    /// Represents a physical mailing address
    /// </summary>
    [Table("Address", Schema = "sis")]
    public class Address : BaseEntity
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
