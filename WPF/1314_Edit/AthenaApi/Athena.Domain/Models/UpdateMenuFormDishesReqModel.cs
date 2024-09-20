using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class UpdateMenuFormDishesReqModel
    {
        public DateTime Date { get; set; }

        public int MealTypeSk { get; set; }

        public int MenuFormTypeSk { get; set; }
        public int LocationSk { get; set; }
        public int? SubLocationSk { get; set; }
        public int MenuTypeSk { get; set; }
        public bool ActiveStatus { get; set; }
        public List<CourseDetails>? CourseDetails { get; set; }

        public string UserId { get; set; }
    }
}
