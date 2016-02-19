using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MC.eSIS.Core.Entities.Infrastructure
{
    [Table("UserSalt", Schema = "inf")]
    public class UserSalt : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public virtual User User { get; set; }
    }
}