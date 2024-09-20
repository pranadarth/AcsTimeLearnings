using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class SubDishesReqModel
    {
        public int DishSk { get; set; }
        public int SubDishSk { get; set; }
        public float? Quantity { get; set; }
        public float? Units { get; set; }
        public float? Cost { get; set; }
    }
}
