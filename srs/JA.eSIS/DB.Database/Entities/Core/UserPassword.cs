using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JA.eSIS.DB.Database.Entities.Core
{
    [Table("UserPassword", Schema = "core")]
    public class UserPassword : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public virtual User User { get; set; }
    }
}