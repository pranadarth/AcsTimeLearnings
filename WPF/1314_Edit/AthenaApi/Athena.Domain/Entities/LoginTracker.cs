using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Login_Tracker")]
    public class LoginTracker
    {
        [Key]
        [Column("Login_Tracker_Id")]
        public int LoginTrackerId { get; set; }
        [Column("User_Sk")]
        [Required]
        public int UserSk { get; set; }
        [Column("User_ID")]
        [MaxLength(60)]
        public string? UserId { get; set; }
        [Column("Login_Date")]
        public DateTime? LoginDate { get; set; }
    }
}
