using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class SaveDishIngSubstitutionReqModel
    {
        public long PreIngSk { get; set; }
        public long SubstituteWithIngSk { get; set; }

        public List<SubstituteOnDish> SubstituteOnDish { get; set; }
        public string UserId { get; set; }
    }

    public class SubstituteOnDish
    {
        public int DishSk { get; set; }
        public float PreSaleCost { get; set; }
        public float PostSaleCost { get; set; }
    }
}
