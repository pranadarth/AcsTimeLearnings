using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DishModel
    {
        public DishGenerationInfoModel? DishGenerationInfo { get; set; }
        public List<DishIngredientModel>? DishIngredients { get; set; }
        public List<SubDishesModel>? SubDishes { get; set; }
        public List<DishPreparationsModel>? DishPreparations { get; set; }

        public float? SalePrice { get; set; }
        public float? DishCost { get; set; }
        public float? CostPerPortion { get; set; }
        public float? LabourUtility { get; set; }
        public float? PortionSize { get; set; }
        public bool CrossContamination { get; set; }
    }
}
