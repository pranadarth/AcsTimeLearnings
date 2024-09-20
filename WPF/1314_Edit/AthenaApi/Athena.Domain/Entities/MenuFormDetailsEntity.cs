using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Menu_Form_Details")]
    public class MenuFormDetailsEntity
    {
        [Key]
        [Column("MF_Details_Sk")]
        public int MenuFormDetailsSk { get; set; }

        [Column("MF_Date")]
        [Required]
        public DateTime MenuFormDate { get; set; }

        [Column("MF_Week_Date")]
        public DateTime? MenuFormWeekDate { get; set; }

        [Column("MFMC_Sk")]
        public int? MenuFormMealCourseSk { get; set; }

        [Column("LocMenuMap_Id")]
        public int? LocationMenuMapId { get; set; }

        [Column("DishCategory_Id")]
        public int? DishCategoryId { get; set; }

        [Column("Dish_Sk")]
        public int? DishSk { get; set; }

        [Column("Location_Sk")]
        public int? LocationSk { get; set; }

        [Column("Sub_Location_Sk")]
        public int? SubLocationSk { get; set; }

        [Column("MealType_ID")]
        [Required]
        public int MealTypeId { get; set; }

        [Column("Course_Type_Sk")]
        [Required]
        public int CourseTypeSk { get; set; }

        [Column("Dish_MenuType_Id")]
        public int? DishMenuTypeId { get; set; }

        [Column("Quantity")]
        public int? Quantity { get; set; } = 0;

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

        [ForeignKey("DishSk")]
        public DishManagerEntity Dish { get; set; }

        [ForeignKey("LocationMenuMapId")]
        public LocationMenuTypeMappingEntity LocationMenuTypeMapping { get; set; }

        [ForeignKey("MenuFormMealCourseSk")]
        public MenuFormMealCourseMappingEntity MenuFormMealCourseMapping { get; set; }
    }
}
