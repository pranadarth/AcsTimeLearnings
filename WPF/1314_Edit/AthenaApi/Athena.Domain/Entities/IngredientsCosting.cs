using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredients_Costing")]
    public class IngredientsCosting
    {
        [Key]
        [Column("Ing_Cost_Sk")]
        public int IngredientCostSk { get; set; }
        [Column("Ing_SK")]
        public long IngSk { get; set; }
        [Column("Unit_Price")]
        public Single? UnitPrice { get; set; }
        [Column("Pack_Size")]
        [MaxLength(100)]
        public string? PackSize { get; set; }
        [Column("Pack_cost")]
        public float? PackCost { get; set; }
        public System.DateTime? SysStartTime { get; set; }
        public System.DateTime? SysEndTime { get; set; }

        [ForeignKey("IngSk")]
        public IngredientsMaster? IngredientMaster { get; set; }

    }
}
