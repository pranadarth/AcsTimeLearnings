using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Menu_Form_Allowance")]
    public class MenuFormAllowanceEntity
    {
        [Key]
        [Column("MF_Allowance_Sk")]
        public int MenuFormAllowanceSk { get; set; }

        [Column("MFMC_Sk")]
        public int MenuFormMealCourseSk { get; set; }

        [Column("Allowance_Qty")]
        public int AllowanceQty { get; set; }

        [Column("Active_Status")]
        public bool ActiveStatus { get; set; } = true;

        [Column("Created_By")]
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "SYSTEM";

        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("Modified_By")]
        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }

        [Column("SysStartTime")]
        public DateTime SysStartTime { get; set; }

        [Column("SysEndTime")]
        public DateTime SysEndTime { get; set; }

        [ForeignKey("MenuFormMealCourseSk")]
        public MenuFormMealCourseMappingEntity MenuFormMealCourseMapping { get; set; }
    }
}