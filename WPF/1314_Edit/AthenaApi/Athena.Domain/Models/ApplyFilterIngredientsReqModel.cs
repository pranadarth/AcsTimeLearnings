using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class ApplyFilterIngredientsReqModel
    {
        public string? SupplierReferenceNo { get; set; }
        public List<string>? SupplierName { get; set; }
        public float? MinimumOrderQuantity { get; set; }
        public List<int>? StoreSk { get; set; }
        public List<int>? IngredientTypeId { get; set; }
        public List<int>? MeasureOptionId { get; set; }
        public int? UnitCost { get; set; }
        public int? PackCost { get; set; }
        public int? Weight { get; set; }
    }
}
