using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IIngredientMasterAllergenRepository
    {
        public Task<List<IngredientsMasterAllergensEntity>> Get(long ingSk);
        public Task<bool> Save(List<SaveIngredientAllergenRequestModel> caloric);

        #region Reports
        public Task<object> GetAllergenHeaderDetails();
        public Task<List<IngredientAllergenModel>> GetAllergenDetails(int supplierId, string status);
        #endregion
    }
}
