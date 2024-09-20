using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class SaveIngredientAllergenRequestModel
    {
        public long IngSk { get; set; }
        public int AllergenId { get; set; }

        [DefaultValue(null)]
        public int? SubAllergenId { get; set; }

        [DefaultValue(null)]
        public int? AllergenOptionId { get; set; }
        public string UserId { get; set; }
    }
}
