using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IIngredientCalorieRepository
    {
        public Task<List<IngredientsMasterCaloric>> Get(long ingSk);
        public Task<bool> Save(List<SaveIngredientCaloricReqModel> caloric);
        public Task<bool> Update(List<UpdateIngredientCaloricInfoReqModel> caloric);

        #region Reports
        public Task<object> GetNutritionalHeaderDetails();
        public Task<List<IngredientCalorieModel>> GetNutritionalDetails(int supplierId, string status);
        #endregion
    }
}
