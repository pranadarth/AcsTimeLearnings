using Athena.Application.BusinessLogic;
using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Common;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Domain.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IIngredientsRepository _ingredientsRepositoryInterface;
        private readonly IIngredientCalorieRepository _ingredientCalorieRepository;
        private readonly IIngredientCalorieRepository _iingredientCalorieRepository;
        private readonly IFoodCaloricTypeRepository _ifoodCaloricTypeRepository;
        private readonly IIngredientMasterAllergenRepository _iingredientAllergenRepository;
        private readonly IAllergenRepository _iAllergenRepository;
        private readonly ISubAllergenRepository _iSubAllergenRepository;
        private readonly IIngredientsLinkingRepository _ingredientsLinkingRepository;


        private readonly IngredientMgmt _ingredientMgmt;

        public IngredientsService(IIngredientsRepository ingredientsRepositoryInterface, IIngredientCalorieRepository ingredientCalorieRepository,
            IIngredientCalorieRepository iingredientCalorieRepository, IFoodCaloricTypeRepository ifoodCaloricTypeRepository,
            IIngredientMasterAllergenRepository iingredientAllergenRepository, IAllergenRepository iAllergenRepository,
            ISubAllergenRepository iSubAllergenRepository, IIngredientsLinkingRepository ingredientsLinkingRepository)
        {
            _ingredientsRepositoryInterface = ingredientsRepositoryInterface;
            _ingredientCalorieRepository = ingredientCalorieRepository;
            _iingredientCalorieRepository = iingredientCalorieRepository;
            _ifoodCaloricTypeRepository = ifoodCaloricTypeRepository;
            _iingredientAllergenRepository = iingredientAllergenRepository;
            _iAllergenRepository = iAllergenRepository;
            _iSubAllergenRepository = iSubAllergenRepository;
            _ingredientsLinkingRepository = ingredientsLinkingRepository;

            _ingredientMgmt = new IngredientMgmt(_ingredientsRepositoryInterface, _ingredientsLinkingRepository);
        }

        public async Task<IngredientsDetailsModel> GetIngredients(string supplierName, int recsPerPage, int currPageNo)
        {
            IngredientsDbModel ings = await _ingredientsRepositoryInterface.GetIngredients(supplierName, recsPerPage, currPageNo);
            if (ings == null)
                return null;


            return await _ingredientMgmt.GetIngredientDetailsModel(ings);
        }

        public async Task<IngredientsDetailsModel> SearchIngredients(string supplierName, string searchText, float? cost, string supplierReferenceNo, IngredientSearchRequestModel reqData, int recsPerPage, int currPageNo, string ingredientName)
        {
            IngredientsDbModel ings = new IngredientsDbModel();
            if (!string.IsNullOrEmpty(ingredientName))
                ings = await _ingredientsRepositoryInterface.GetIngredientsName(supplierName, ingredientName, cost, supplierReferenceNo, reqData, recsPerPage, currPageNo);
            else if (string.IsNullOrEmpty(searchText))
                ings = await _ingredientsRepositoryInterface.GetIngredients(supplierName, recsPerPage, currPageNo);
            else
                ings = await _ingredientsRepositoryInterface.SearchIngredients(supplierName, searchText, cost, supplierReferenceNo, reqData, recsPerPage, currPageNo);

            if (ings == null)
                return null;

            return await _ingredientMgmt.GetIngredientDetailsModel(ings);
        }

        public async Task<object> GetPackUOM()
        {
            return await _ingredientsRepositoryInterface.GetPackUOM();
        }

        public async Task<object> GetFoodgroups()
        {
            return await _ingredientsRepositoryInterface.GetFoodgroups();
        }
        public async Task<object> GetSuppliers()
        {
            return await _ingredientsRepositoryInterface.GetSuppliers();
        }

        public async Task<object> GetCountries()
        {
            return await _ingredientsRepositoryInterface.GetCountries();
        }
        public async Task<object> GetStatus()
        {
            return await _ingredientsRepositoryInterface.GetStatus();
        }
        public async Task<object> GetContractedStatus()
        {
            return await _ingredientsRepositoryInterface.GetContractedStatus();
        }

        public async Task<object> GetIngredientTypes()
        {
            return await _ingredientsRepositoryInterface.GetIngredientTypes();
        }

        public async Task<object> Save(SaveIngredientReqModel reqData)
        {
            object ingsk = await _ingredientsRepositoryInterface.Save(reqData);

            return new
            {
                IngSk = ingsk
            };
        }

        public async Task<IngredientsMaster> GetByIngNameProductCodeAndSupplier(string ingName, string productCode, string supplier)
        {
            return await _ingredientsRepositoryInterface.GetByIngNameProductCodeAndSupplier(ingName, productCode, supplier);
        }

        public async Task<IngredientsMaster> GetByProductCodeAndSupplier(string productCode, string supplier)
        {
            return await _ingredientsRepositoryInterface.GetByProductCodeAndSupplier(productCode, supplier);
        }
        public async Task<object> Edit(EditIngredientReqModel reqData)
        {
            return await _ingredientsRepositoryInterface.Edit(reqData);
        }

        public async Task<List<Ingredients>> GetExportIngredients(GetExportIngredientsReqModel reqData)
        {
            List<IngredientsMaster> ings = await _ingredientsRepositoryInterface.GetExportIngredients(reqData);
            if (ings == null)
                return null;

            IngredientsDbModel ingredientsDbModel = new IngredientsDbModel
            {
                Ingredients = ings
            };
            IngredientsDetailsModel ingredientsDetailsModel = await _ingredientMgmt.GetIngredientDetailsModel(ingredientsDbModel);
            if (ingredientsDetailsModel is not null && ingredientsDetailsModel.Ingredients is not null && ingredientsDetailsModel.Ingredients.Count > 0)
            {
                return ingredientsDetailsModel.Ingredients;
            }
            return null;
        }

        public async Task<IngredientsDetailsModel> ApplyFilterIngredients(ApplyFilterIngredientsReqModel reqData)
        {
            List<IngredientsMaster> ings = await _ingredientsRepositoryInterface.ApplyFilterIngredients(reqData);
            if (ings == null)
                return null;

            IngredientsDbModel ingredientsDbModel = new IngredientsDbModel
            {
                Ingredients = ings
            };

            return await _ingredientMgmt.GetIngredientDetailsModel(ingredientsDbModel);
        }

        public async Task<bool> SaveIngredientLinking(SaveIngredientLinkingReqModel reqData)
        {
            return await _ingredientsLinkingRepository.SaveIngredientLinking(reqData);
        }

        public async Task<object> UpdateGenericYield(long ingSk, long genericYield, string userId)
        {
            return await _ingredientsRepositoryInterface.UpdateGenericYield(ingSk, genericYield, userId);
        }

        public async Task<List<Ingredients>> GetIngredientLinkings(long ingSk)
        {
            List<IngredientsLinkingEntity> ingLinks = await _ingredientsLinkingRepository.GetIngredientLinking(ingSk);
            if (ingLinks == null)
                return null;

            List<long> ings = ingLinks.Select(x => x.DestIngSk).ToList();
            List<IngredientsMaster> ingredientDetails = await _ingredientsRepositoryInterface.GetIngredients(ings);

            IngredientsDbModel ingredientsDbModel = new IngredientsDbModel
            {
                Ingredients = ingredientDetails
            };

            IngredientsDetailsModel ingredientsDetailsModel = await _ingredientMgmt.GetIngredientDetailsModel(ingredientsDbModel);

            if (ingredientsDetailsModel is not null && ingredientsDetailsModel.Ingredients is not null && ingredientsDetailsModel.Ingredients.Count > 0)
            {
                return ingredientsDetailsModel.Ingredients;
            }
            return null;
        }

        #region Reports
        public async Task<object> GetDeclarationHeaderDetails()
        {
            return await _ingredientsRepositoryInterface.GetDeclarationHeaderDetails();
        }
        public async Task<List<Ingredients>> GetDeclarationDetails(int supplierId, string status)
        {
            List<DeclarationStausDetailsModel> ings = await _ingredientsRepositoryInterface.GetDeclarationDetails(supplierId, status);
            if (ings == null || ings.Count() < 1)
                return null;

            return await _ingredientMgmt.GetIngredientDelcarationStatusModel(ings);
        }


        public async Task<object> GetNutritionalHeaderDetails()
        {
            return await _ingredientCalorieRepository.GetNutritionalHeaderDetails();
        }

        public async Task<object> GetNutritionalDetails(int supplierId, string status)
        {
            List<IngredientCalorieModel> ings = await _ingredientCalorieRepository.GetNutritionalDetails(supplierId, status);
            if (ings == null)
                return null;

            IngredientCaloricMgmt ingredientCaloricMgmt = new IngredientCaloricMgmt(_iingredientCalorieRepository, _ifoodCaloricTypeRepository);
            List<object> caloricDetails = ingredientCaloricMgmt.GetCaloricDetailsModel(ings);

            return caloricDetails;
        }

        public async Task<object> GetNutritionalDetailsWithoutGrouping(int supplierId, string status)
        {
            List<IngredientCalorieModel> ings = await _ingredientCalorieRepository.GetNutritionalDetails(supplierId, status);
            if (ings == null)
                return null;

            IngredientCaloricMgmt ingredientCaloricMgmt = new IngredientCaloricMgmt(_iingredientCalorieRepository, _ifoodCaloricTypeRepository);
            List<object> caloricDetails = ingredientCaloricMgmt.GetCaloricDetailsModelWithoutGrouping(ings);

            return caloricDetails;
        }

        public async Task<object> GetAllergenHeaderDetails()
        {
            return await _iingredientAllergenRepository.GetAllergenHeaderDetails();
        }

        public async Task<object> GetAllergenDetails(int supplierId, string status)
        {
            List<IngredientAllergenModel> ings = await _iingredientAllergenRepository.GetAllergenDetails(supplierId, status);
            if (ings == null)
                return null;

            IngredientAllergenMgmt ingredientAllergenMgmt = new IngredientAllergenMgmt(_iingredientAllergenRepository, _iAllergenRepository, _iSubAllergenRepository);
            List<object> caloricDetails = ingredientAllergenMgmt.GetAllergenDetailsModel(ings);

            return caloricDetails;
        }
        #endregion
    }
}
