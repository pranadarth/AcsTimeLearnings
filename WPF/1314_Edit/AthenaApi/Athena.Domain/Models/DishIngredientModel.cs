using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
   
    public class DishIngredientModel
    {
        public int DishIngSk { get; set; }

        public int DishSk { get; set; }
        public string? IngredientName { get; set; }

        public long IngSk { get; set; }

        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }

        public float? Quantity { get; set; }

        public int? MeasureOptionId { get; set; }

        public float? Weight { get; set; }

        public float? UnitCost { get; set; }

        public float? CostExtension { get; set; }
    }
}
