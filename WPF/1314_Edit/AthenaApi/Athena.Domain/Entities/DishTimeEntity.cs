using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Time")]
    public class DishTimeEntity : BaseEntity
    {
        [Key]
        [Column("Dish_Time_Sk")]
        public int DishTimeSk { get; set; }

        [Column("Dish_Time_Code")]
        [MaxLength(30)]
        public string? DishTimeCode { get; set; }

        [Column("Dish_Time_Description")]
        [MaxLength(200)]
        public string? DishTimeDescription { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
    }
}
