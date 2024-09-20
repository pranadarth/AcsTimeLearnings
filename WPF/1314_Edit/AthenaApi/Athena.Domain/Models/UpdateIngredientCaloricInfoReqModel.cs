using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class UpdateIngredientCaloricInfoReqModel
    {
        public long IngSk { get; set; }
        public int CaloricTypeSk { get; set; }
        public double? Value { get; set; }

        public string UserId { get; set; }
    }
}
