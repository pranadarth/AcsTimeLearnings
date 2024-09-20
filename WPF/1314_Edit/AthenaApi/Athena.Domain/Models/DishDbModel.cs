using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DishDbModel
    {
        public List<DishManagerEntity>? Dishes { get; set; }
        public long TotalDishes { get; set; }
    }
}
