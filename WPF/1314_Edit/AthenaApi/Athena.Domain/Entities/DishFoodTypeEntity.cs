using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Food_Type")]
    public class DishFoodTypeEntity : BaseEntity
    {
        [Key]
        [Column("Dish_FoodType_Id")]
        public int DishFoodTypeId { get; set; }

        [Required]
        [Column("Dish_FoodType_Code")]
        [MaxLength(25)]
        public string DishFoodTypeCode { get; set; }

        [Required]
        [Column("Dish_FoodType_Desc")]
        [MaxLength(50)]
        public string DishFoodTypeDesc { get; set; }

        [Required]
        [Column("Display_Order")]
        public int DisplayOrder { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }
    }
}
