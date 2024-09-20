using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Sub_Dishes")]
    public class DishSubDishEntity
    {
        [Key]
        [Column("DishSubDish_Sk")]
        public int DishSubDishSk { get; set; }

        [Column("Dish_Sk")]
        [Required]
        public int DishSk { get; set; }

        [Column("Sub_Dish_Sk")]
        [Required]
        public int SubDishSk { get; set; }

        [Column("Quantity")]
        public float? Quantity { get; set; }

        [Column("Units")]
        public float? Units { get; set; }

        [Column("Cost")]
        public float? Cost { get; set; }

        [Column("SysStartTime")]
        [Required]
        public DateTime SysStartTime { get; set; }

        [Column("SysEndTime")]
        [Required]
        public DateTime SysEndTime { get; set; }
    }
}
