using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MC.eSIS.Core.Entities
{
    /// <summary>
    /// Represents a physical mailing address and some 
    /// of the common properties that are associated with it.
    /// </summary>
    [Table("Address", Schema = "sis")]
    public class Address : BaseEntity
    {
        //TODO: support addresses outside of the US

        [StringLength(60)]
        public string StreetAddress { get; set; }

        [StringLength(45)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(9)]
        public string ZipCode { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(10)]
        public string AlternatePhoneNumber { get; set; }

        [StringLength(60)]
        public string EmailAddress { get; set; }

        [StringLength(60)]
        public string AlternateEmailAddress { get; set; }
    }
}
