using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Temperature")]
    public class DishTemperatureEntity : BaseEntity
    {
        [Key]
        [Column("Dish_Temp_Sk")]
        public int DishTempSk { get; set; }

        [Column("Dish_Temp_Code")]
        [MaxLength(5)]
        public string? DishTempCode { get; set; }

        [Column("Dish_Temp_Description")]
        [MaxLength(30)]
        public string? DishTempDescription { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
    }
}
