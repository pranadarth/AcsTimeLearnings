using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class GetExportDishesReqModel
    {
        public List<int>? MenuTypeIds { get; set; }
        public List<int>? MealTypeIds { get; set; }
        public List<int>? FoodTypeIds { get; set; }
        public List<int>? LabelTypeIds { get; set; }

        public List<int>? TemplateIds { get; set; }

        public List<int>? PortionControlIds { get; set; }
        public List<int>? SpiceLevelIds { get; set; }
    }
}
