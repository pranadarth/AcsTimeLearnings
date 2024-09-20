using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Meal_Type")]
    public class DishMealTypeEntity : BaseEntity
    {
        [Key]
        [Column("MealType_ID")]
        public int MealTypeId { get; set; }

        [Column("MealType_Code")]
        [MaxLength(25)]
        public string MealTypeCode { get; set; }

        [Column("MealType_Desc")]
        [MaxLength(50)]
        public string MealTypeDesc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }

    }
}
