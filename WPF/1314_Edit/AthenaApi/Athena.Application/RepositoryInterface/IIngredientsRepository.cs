using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Domain.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IIngredientsRepository
    {
        public Task<IngredientsDbModel> GetIngredients(string supplierName, int recsPerPage, int currPageNo);

        public Task<List<IngredientsMaster>> GetIngredients(List<long> ingSks);
        public Task<IngredientsDbModel> SearchIngredients(string supplierName, string searchText, float? cost, string supplierReferenceNo, IngredientSearchRequestModel reqData, int recsPerPage, int currPageNo);

        public Task<List<MeasureOptions>> GetPackUOM();

        public Task<List<FoodGroup>> GetFoodgroups();
        public Task<List<Supplier>> GetSuppliers();

        public Task<List<Country>> GetCountries();
        public Task<List<IngredientStatus>> GetStatus();
        public Task<List<IngredientContractStatus>> GetContractedStatus();
        public Task<List<IngredientTypeEntity>> GetIngredientTypes();

        public Task<object> Save(SaveIngredientReqModel reqData);

        public Task<IngredientsMaster> GetByIngNameProductCodeAndSupplier(string ingName, string productCode, string supplier);
        public Task<IngredientsMaster> GetByProductCodeAndSupplier(string productCode, string supplier);
        public Task<object> Edit(EditIngredientReqModel reqData);
        public Task<object> UpdateGenericYield(long ingSk, long genericYield, string userId);

        
        public Task<List<IngredientsMaster>> GetExportIngredients(GetExportIngredientsReqModel reqData);
        public Task<List<IngredientsMaster>> ApplyFilterIngredients(ApplyFilterIngredientsReqModel reqData);
        public Task<IngredientsDbModel> GetIngredientsName(string supplierName, string ingredientName, float? cost, string supplierReferenceNo, IngredientSearchRequestModel reqData, int recsPerPage, int currPageNo);

        #region Reports
        public Task<object> GetDeclarationHeaderDetails();
        public Task<List<DeclarationStausDetailsModel>> GetDeclarationDetails(int supplierId, string status);


        #endregion
    }
}
