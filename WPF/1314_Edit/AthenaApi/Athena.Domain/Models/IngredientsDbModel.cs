using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class IngredientsDbModel
    {
        public List<IngredientsMaster>? Ingredients { get; set; }
        public long TotalIngredients { get; set; }
    }
}
