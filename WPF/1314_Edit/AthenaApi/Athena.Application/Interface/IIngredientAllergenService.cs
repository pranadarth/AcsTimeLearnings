using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IIngredientAllergenService
    {
        public Task<object> GetAllergenOptions();
        public Task<List<IngredientAllergenModel>> Get(long ingSk);
        public Task<bool> Save(List<SaveIngredientAllergenRequestModel> allergen);
    }
}
