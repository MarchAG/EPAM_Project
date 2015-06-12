using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    [Table("UserProfile")]
    public partial class UserProfile
    {
        public int UserProfileId { get; set; }

        [Required]
        [StringLength(56)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        public virtual List<Article> Articles { get; set; }

        public virtual List<Mark> Marks { get; set; }
    }
}