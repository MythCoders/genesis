using System.ComponentModel.DataAnnotations;

namespace eSIS.Core.Entities.SIS
{
    public class Student : BaseEntity
    {
        public int AddressId { get; set; }

        [MaxLength(45)]
        public string FirstName { get; set; }

        [MaxLength(45)]
        public string MiddleName { get; set; }

        [MaxLength(45)]
        public string LastName { get; set; }

        [MaxLength(1)]
        public char Sex { get; set; }

        public virtual Address Address { get; set; }

        public bool IsFemale()
        {
            return !IsMale();
        }

        public bool IsMale()
        {
            return Sex == 'M';
        }
    }
}