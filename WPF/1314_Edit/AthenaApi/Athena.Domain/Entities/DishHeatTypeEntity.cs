using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Heat_Type")]
    public class DishHeatTypeEntity : BaseEntity
    {
        [Key]
        [Column("Dish_HeatType_Id")] // Assuming there's an ID column, you can change this if necessary
        public int DishHeatTypeId { get; set; } // Assuming an identity/primary key column

        [Required]
        [Column("Dish_HeatType_Code")]
        [MaxLength(25)]
        public string? DishHeatTypeCode { get; set; }

        [Required]
        [Column("Dish_HeatType_Desc")]
        [MaxLength(50)]
        public string? DishHeatTypeDesc { get; set; }

        [Required]
        [Column("Dish_HeatType_Value")]
        public int? DishHeatTypeValue { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }
    }
}
