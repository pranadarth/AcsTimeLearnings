using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Key]
        [Column("User_Sk")]
        public int UserSk { get; set; }
        [Column("User_ID")]
        public string? UserId { get; set; }
        public string? Password { get; set; }
        [Column("First_Name")]
        public string? FirstName { get; set; }
        [Column("Middle_Name")]
        public string? MiddleName { get; set; }
        [Column("Last_Name")]
        public string? LastName { get; set; }
        [Column("Email_ID")]
        public string? EmailId { get; set; }
        [Column("Phone_Number")]
        public string? PhoneNumber { get; set; }
        public string? Signature { get; set; }
        [Column("Address_Line_1")]
        public string? AddressLine1 { get; set; }
        [Column("Address_Line_2")]
        public string? AddressLine2 { get; set; }
        [Column("Address_Line_3")]
        public string? AddressLine3 { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        [Column("Postal_Code")]
        public string? PostalCode { get; set; }
        public string? Sex { get; set; }
        public string? Ethnicity { get; set; }
        [Column("Age_Range")]
        public string? AgeRange { get; set; }
        [Column("Alert_Msg")]
        public short? AlertMsg { get; set; }
        public System.DateTime? DateOfBirth { get; set; }
        [Column("Start_Date")]
        public System.DateTime? StartDate { get; set; }
        [Column("Learner_Code")]
        public string? LearnerCode { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
    }
}
