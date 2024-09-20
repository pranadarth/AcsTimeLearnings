using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DishGenerationInfoReqModel
    {
        public int DishSk { get; set; }
        public string? DishName { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public List<int>? CategoryIds { get; set; }

        public List<int>? DishMealTypeIds { get; set; }

        public List<int>? DishMenuTypeIds { get; set; }

        public int? DishFoodTypeId { get; set; }

        public List<int>? DishLabelTypeIds { get; set; }
        public int? DishTempId { get; set; }

        public int? DishSpiceLevelId { get; set; }

        public int? PortionControlId { get; set; }

        public bool ActiveStaus { get; set; }

        public float? CookedPortionWeight { get; set; }
        public float? Calories { get; set; }
        public float? Protein { get; set; }
        public float? Salt { get; set; }
        public float? Fat { get; set; }
        public float? Fibre { get; set; }
        public float? Sodium { get; set; }
        public float? Carbohydrates { get; set; }
        public float? Popularity { get; set; }
        public float? CalsFromFat { get; set; }
        public string? CalrorieSource { get; set; }
        public float? SaltEquivalent { get; set; }
        public float? SaturatedFat { get; set; }
        public string? PopularitySource { get; set; }
    }
}
