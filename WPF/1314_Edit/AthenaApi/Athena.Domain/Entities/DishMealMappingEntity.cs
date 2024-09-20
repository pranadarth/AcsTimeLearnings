using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Meal_Mapping")]
    public class DishMealMappingEntity
    {
        [Key]
        [Column("Dish_Meal_Mapping_Sk")]
        public int DishMealMappingSk { get; set; }

        [ForeignKey("DishManager")]
        [Column("Dish_Sk")]
        public int? DishSk { get; set; }

        [ForeignKey("DishMealType")]
        [Column("MealType_ID")]
        public int? MealTypeId { get; set; }

        [Column("Active_Status")]
        public bool ActiveStatus { get; set; } = true;

        [Required]
        [Column("SysStartTime")]
        public DateTime SysStartTime { get; set; }

        [Required]
        [Column("SysEndTime")]
        public DateTime SysEndTime { get; set; }

        public DishManagerEntity DishManager { get; set; }

        public DishMealTypeEntity DishMealType { get; set; }
    }
}
