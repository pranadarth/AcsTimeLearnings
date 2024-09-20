using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class IngredientSearchRequestModel
    {
        public List<int>? MeasureOptionIds { get; set; }
        public List<int>? SupplierIds { get; set; }
        public List<int>? AllergenIds { get; set; }
    }
}
