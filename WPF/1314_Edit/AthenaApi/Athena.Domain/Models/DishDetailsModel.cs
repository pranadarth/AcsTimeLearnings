using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DishDetailsModel
    {
        public List<Dish>? Dishes { get; set; }
        public long TotalDishes { get; set; }
    }

    public class Dish
    {
        public int DishSk { get; set; }

        public string? DishName { get; set; }

        public string? DisplayName { get; set; }

        //public string? DishPreparation { get; set; }

        public List<DishMealTypeModel>? DishMealTypes { get; set; }

        public List<DishMenuTypeModel>? DishMenuTypes { get; set; }

        public int? DishFoodTypeId { get; set; }

        public int? DishSpiceLevelId { get; set; }

        public int? PortionControlId { get; set; }

        public int? DishTempId { get; set; }

        public int? DishShelfLifeId { get; set; }

        public float? PortionSize { get; set; }

        public float? CookedPortionWeight { get; set; }

        public float? SalePrice { get; set; }

        public bool CrossContamination { get; set; }

        public bool ActiveStatus { get; set; }

        public string? DishImage { get; set; }

        public List<DishCategoryModel> DishCategories { get; set; }

        //public float? PreparationTime { get; set; }

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
        public string? DishFoodTypeCode { get; set; }
        public string? DishHeatTypeCode { get; set; }
        public float? CostPerPortion { get; set; }
        public float? Cost { get; set; }
    }
}
