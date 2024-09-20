using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Ingredients")]
    public class DishIngredientEntity
    {
        [Key]
        [Column("Dish_Ing_Sk")]
        public int DishIngSk { get; set; }

        [Column("Dish_Sk")]
        [Required]
        public int DishSk { get; set; }

        [Column("Ing_Sk")]
        [Required]
        public long IngSk { get; set; }

        [Column("Supplier_Id")]
        public int? SupplierId { get; set; }

        [Column("Quantity")]
        public float? Quantity { get; set; }

        [Column("Measure_Option_Id")]
        public int? MeasureOptionId { get; set; }

        [Column("Weight")]
        public float? Weight { get; set; }

        [Column("Unit_Cost")]
        public float? UnitCost { get; set; }

        [Column("Cost_Extension")]
        public float? CostExtension { get; set; }

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
