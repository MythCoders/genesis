using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//using System.Web.Mvc;

namespace eSIS.Core.Entities
{
    /// <summary>
    /// All database entites, MUST inherit from this class.
    /// This will ensure every record can be tracked the same
    /// </summary>
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Primary key for record
        /// </summary>
        [Required]
        [Key]
        //[HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [MaxLength(10)]
        [Index]
        public string SystemCode { get; set; }

        /// <summary>
        /// User who created the record
        /// </summary>
        [StringLength(60)]
        //[HiddenInput(DisplayValue = false)]
        public string AddUser { get; set; }

        /// <summary>
        /// Timestamp of when the record was created
        /// </summary>
        //[HiddenInput(DisplayValue = false)]
        public DateTime? AddDate { get; set; }

        /// <summary>
        /// Most recent user to update the record
        /// </summary>
        [StringLength(60)]
        //[HiddenInput(DisplayValue = false)]
        public string ModUser { get; set; }

        /// <summary>
        /// Timestamp of the most recent time the record was updated
        /// </summary>
        //[HiddenInput(DisplayValue = false)]
        public DateTime? ModDate { get; set; }

        /// <summary>
        /// Entity framework concurrency-check field. This will ensure
        /// that when a user updates a record, the database will check to make sure
        /// that the user is updating the most recent version of said record. If not,
        /// the update fails and the user is presented with the latest version so he/she 
        /// can make his/her changes again.
        /// </summary>
        [Timestamp]
        //[HiddenInput(DisplayValue = false)]
        // byte[] is the rowversion/timestamp .NET type
        public byte[] Version { get; set; }
    }
}
