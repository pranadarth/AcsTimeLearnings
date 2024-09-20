using Athena.Domain.Common;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IIngredientsService
    {
        public Task<IngredientsDetailsModel> GetIngredients(string supplierName, int recsPerPage, int currPageNo);
        public Task<IngredientsDetailsModel> SearchIngredients(string supplierName, string searchText, float? cost, string supplierReferenceNo, IngredientSearchRequestModel reqData, int recsPerPage, int currPageNo, string ingredientName);
        public Task<object> GetPackUOM();

        public Task<object> GetFoodgroups();
        public Task<object> GetSuppliers();

        public Task<object> GetCountries();
        public Task<object> GetStatus();
        public Task<object> GetContractedStatus();

        public Task<object> GetIngredientTypes();

        public Task<object> Save(SaveIngredientReqModel reqData);

        public Task<IngredientsMaster> GetByIngNameProductCodeAndSupplier(string ingName, string productCode, string supplier);
        public Task<IngredientsMaster> GetByProductCodeAndSupplier(string productCode, string supplier);
        public Task<object> Edit(EditIngredientReqModel reqData);
        public Task<List<Ingredients>> GetExportIngredients(GetExportIngredientsReqModel reqData);
        public Task<IngredientsDetailsModel> ApplyFilterIngredients(ApplyFilterIngredientsReqModel reqData);
        public Task<bool> SaveIngredientLinking(SaveIngredientLinkingReqModel reqData);
        public Task<object> UpdateGenericYield(long ingSk, long genericYield, string userId);

        public Task<List<Ingredients>> GetIngredientLinkings(long ingSk);

        #region Reports
        public Task<object> GetDeclarationHeaderDetails();
        public Task<List<Ingredients>> GetDeclarationDetails(int supplierId, string status);

        public Task<object> GetNutritionalHeaderDetails();
        public Task<object> GetNutritionalDetails(int supplierId, string status);
        public Task<object> GetNutritionalDetailsWithoutGrouping(int supplierId, string status);

        public Task<object> GetAllergenHeaderDetails();
        public Task<object> GetAllergenDetails(int supplierId, string status);
        #endregion

    }
}
