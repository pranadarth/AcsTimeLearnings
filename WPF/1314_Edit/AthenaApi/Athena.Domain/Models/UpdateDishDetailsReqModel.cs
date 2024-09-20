using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class UpdateDishDetailsReqModel
    {
        public DishGenerationInfoReqModel? DishGenerationInfo { get; set; }
        public List<DishIngredientReqModel>? DishIngredients { get; set; }
        public List<SubDishesReqModel>? SubDishes { get; set; }
        public List<DishPreparationsReqModel>? DishPreparations { get; set; }

        public float? SalePrice { get; set; }
        public float? DishCost { get; set; }
        public float? CostPerPortion { get; set; }
        public float? LabourUtility { get; set; }
        public int? PortionSize { get; set; }
        public bool CrossContamination { get; set; }

        public string? UserId { get; set; }
    }
}
