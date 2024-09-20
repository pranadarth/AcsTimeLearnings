using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class MenuFormCourseMappingModel
    {
        public DateTime MenuFormDate { get; set; }
        public int MealTypeId { get; set; }
        public int MenuFormMealCourseSk { get; set; }
    }
}
