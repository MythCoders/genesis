﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MC.eSIS.Core.Entities.Infrastructure
{
    [Table("User", Schema = "inf")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(60)]
        public string EmailAddress { get; set; }

        public string AccessToken { get; set; }

        public DateTime? AccessTokenCreateDate { get; set; }

        public bool? AccessTokenIsUsed { get; set; }

        public int ResetQuestion1Id { get; set; }

        public int ResetQuestion2Id { get; set; }

        [Required]
        public string ResetQuestion1Answer { get; set; }

        [Required]
        public string ResetQuestion2Answer { get; set; }

        public DateTime? ActivationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int AccountStatus { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        [StringLength(35)]
        public string FirstName { get; set; }

        [StringLength(2)]
        public string MI { get; set; }

        [Required]
        [StringLength(35)]
        public string LastName { get; set; }

        public virtual ResetQuestion ResetQuestion1 { get; set; }
        public virtual ResetQuestion ResetQuestion2 { get; set; }
        public virtual List<UserPassword> UserPasswords { get; set; }
        public virtual List<UserSalt> UserSalts { get; set; }

    }
}