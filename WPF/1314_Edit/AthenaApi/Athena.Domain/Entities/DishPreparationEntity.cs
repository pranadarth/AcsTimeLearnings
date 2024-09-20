using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Preparation")]
    public class DishPreparationEntity
    {
        [Key]
        [Column("Dish_Prep_Sk")]
        public int DishPrepSk { get; set; }

        [Column("Dish_Sk")]
        [Required]
        public int DishSk { get; set; }

        [Column("Dish_Prep_Step_Sequence")]
        public int? DishPrepStepSequence { get; set; }

        [Column("Dish_Prep_Method")]
        [MaxLength(500)]
        public string DishPrepMethod { get; set; }

        [Column("Dish_Processes_Sk")]
        public int? DishProcessesSk { get; set; }

        [Column("Dish_Proc_Step_Sk")]
        public int? DishProcStepSk { get; set; }

        [Column("Dish_Proc_Section_Sk")]
        public int? DishProcSectionSk { get; set; }

        [Column("Dish_Time_Sk")]
        public int? DishTimeSk { get; set; }

        [Column("Dish_Prep_Time")]
        public float? DishPrepTime { get; set; }

        [Column("Dish_Temp_Sk")]
        public int? DishTempSk { get; set; }

        [Column("Dish_Low_Temp")]
        public float? DishLowTemp { get; set; }

        [Column("Dish_High_Temp")]
        public float? DishHighTemp { get; set; }

        [Column("Dish_HACCP_Flag")]
        public bool? DishHaccpFlag { get; set; }

        [Column("Modified_By")]
        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

        [Column("SysStartTime")]
        [Required]
        public DateTime SysStartTime { get; set; }

        [Column("SysEndTime")]
        [Required]
        public DateTime SysEndTime { get; set; }
    }
}
