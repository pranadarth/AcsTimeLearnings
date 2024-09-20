using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DeclarationStausDetailsModel
    {
        public IngredientsMaster Ingredient { get; set; }
        public string DeclartaionStatusDescription { get; set; }
    }
}
