using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class IngredientSubAllergenModel
    {
        public int IngMasAllergenSk { get; set; }
        public long IngSk { get; set; }
        public int AllergenId { get; set; }
        public int SubAllergenId { get; set; }
        public int? AllergenOptionId { get; set; }
        public string SubAllergenName { get; set; }
    }
}
