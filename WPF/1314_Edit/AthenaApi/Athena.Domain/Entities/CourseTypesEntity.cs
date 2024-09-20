using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Course_Types")]
    public class CourseTypesEntity
    {
        [Key]
        [Column("Course_Type_Sk")]
        public int CourseTypeSk { get; set; }

        [Column("Course_Code")]
        [MaxLength(30)]
        public string CourseCode { get; set; }

        [Column("Course_Desc")]
        [MaxLength(200)]
        public string CourseDesc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Column("Active_Status")]
        public bool ActiveStatus { get; set; } = true;

        [Column("Created_By")]
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "SYSTEM";

        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("Modified_By")]
        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
