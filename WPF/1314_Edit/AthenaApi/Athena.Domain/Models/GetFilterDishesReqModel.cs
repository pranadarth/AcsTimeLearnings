using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class GetFilterDishesReqModel
    {
        public List<int>? CategoryIds { get; set; }
        public List<float>? PortionSize { get; set; }
        public List<float>? CostPerDish { get; set; }
        public List<int>? SpiceLevelIds { get; set; }
        public List<int>? MenuTypeIds { get; set; }
        public List<int>? MealTypeIds { get; set; }
        public List<int>? FoodTypeIds { get; set; }
        public List<int>? LabelTypeIds { get; set; }
    }
}
