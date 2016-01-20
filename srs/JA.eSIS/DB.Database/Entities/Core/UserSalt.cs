using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Database.Core.Entities.Core
{
    [Table("UserSalt", Schema = "core")]
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