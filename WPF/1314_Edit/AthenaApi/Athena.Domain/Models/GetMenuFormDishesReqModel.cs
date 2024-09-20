using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class GetMenuFormDishesReqModel
    {
        public DateTime Date { get; set; }
        public int MealTypeId { get; set; }
        public int MenuFormTypeSk { get; set; }
        public int LocationSk { get; set; }
        public int? SubLocationSk { get; set; }
        public int MenuTypeSk { get; set; }
    }
}
