using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class AddMenuFormDetailsReqModel
    {
        public DateTime MenuFormDate { get; set; }
        public DateTime? MenuFormWeekDate { get; set; }
        public int? MenuFormMealCourseSk { get; set; }
        public int? LocationMenuMapId { get; set; }
        public int? DishSk { get; set; }
        public int MealTypeId { get; set; }
        public int? DishMenuTypeId { get; set; }
        public int? Quantity { get; set; } = 0;
        public string UserId { get; set; }
        public bool ActiveStatus { get; set; } = true;
    }
}
