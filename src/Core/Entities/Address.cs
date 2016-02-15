using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities
{
    /// <summary>
    /// Represents a physical mailing address and some 
    /// of the common properties that are associated with it.
    /// </summary>
    [Table("Address", Schema = "sis")]
    public class Address : BaseEntity
    {
        [StringLength(60)]
        [Display(Name = "street Address")]
        public string FirstAddressLine { get; set; }

        [StringLength(60)]
        [Display(Name = "Po Box")]
        public string SecondAddressLine { get; set; }

        [StringLength(45)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(20)]
        [Display(Name = "State")]
        public string Region { get; set; }

        [StringLength(9)]
        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Country")]
        public string County { get; set; }

        [StringLength(10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(10)]
        [Display(Name = "Alternate Phone Number")]
        public string AlternatePhoneNumber { get; set; }

        [StringLength(60)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [StringLength(60)]
        [Display(Name = "Alternate Email Address")]
        public string AlternateEmailAddress { get; set; }
    }
}
