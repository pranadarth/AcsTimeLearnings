using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("MenuForm_Meal_Course_Mapping")]
    public class MenuFormMealCourseMappingEntity
    {
        [Key]
        [Column("MFMC_Sk")]
        public int MenuFormMealCourseSk { get; set; }

        [Column("Menu_Form_Type_Sk")]
        public int? MenuFormTypeSk { get; set; }

        [Column("MealType_ID")]
        public int? MealTypeId { get; set; }

        [Column("Course_Type_Sk")]
        public int? CourseTypeSk { get; set; }

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
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }

        [Column("SysStartTime")]
        public DateTime SysStartTime { get; set; }

        [Column("SysEndTime")]
        public DateTime SysEndTime { get; set; }

        [ForeignKey("CourseTypeSk")]
        public CourseTypesEntity CourseType { get; set; }

        [ForeignKey("MealTypeId")]
        public DishMealTypeEntity MealType { get; set; }

        [ForeignKey("MenuFormTypeSk")]
        public MenuFormTypeEntity MenuFormType { get; set; }
    }
}
