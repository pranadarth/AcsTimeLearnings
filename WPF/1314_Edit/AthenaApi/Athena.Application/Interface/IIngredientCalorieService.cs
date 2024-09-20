using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IIngredientCalorieService
    {
        public Task<List<IngredientCalorieModel>> Get(long ingSk);
        public Task<bool> Save(List<SaveIngredientCaloricReqModel> caloric);
        public Task<bool> Update(List<UpdateIngredientCaloricInfoReqModel> caloric);
    }
}
