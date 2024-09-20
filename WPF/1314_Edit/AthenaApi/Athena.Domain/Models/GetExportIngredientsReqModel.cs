using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class GetExportIngredientsReqModel
    {
        public List<string>? SupplierName { get; set; }
        public List<int>? StoreSk { get; set; }
        public List<int>? FoodGroupSk { get; set; }
        public List<int>? IngredientTypeId { get; set; }
        public bool IsRetailUsage { get; set; } = false;
    }
}
