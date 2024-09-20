using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Domain.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.BusinessLogic
{
    public class IngredientMgmt
    {
        private readonly IIngredientsRepository _ingredientsRepositoryInterface;
        private readonly IIngredientsLinkingRepository _ingredientsLinkingRepository;

        public IngredientMgmt(IIngredientsRepository iingredientsRepository, IIngredientsLinkingRepository ingredientsLinkingRepository)
        {
            _ingredientsRepositoryInterface = iingredientsRepository;
            _ingredientsLinkingRepository = ingredientsLinkingRepository;
        }

        public async Task<IngredientsDetailsModel> GetIngredientDetailsModel(IngredientsDbModel ingredientsDbModel)
        {
            List<IngredientsMaster> ings = ingredientsDbModel.Ingredients;
            if (ings == null || ings.Count < 1)
                return null;

            List<FoodGroup> foodGroups = await _ingredientsRepositoryInterface.GetFoodgroups();
            List<Country> countries = await _ingredientsRepositoryInterface.GetCountries();
            List<IngredientStatus> status = await _ingredientsRepositoryInterface.GetStatus();
            List<IngredientContractStatus> contractedStatus = await _ingredientsRepositoryInterface.GetContractedStatus();

            List<long> ingSks = ings.Select(x => x.IngSk).ToList();
            List<long> ingredientLinkedSks = await _ingredientsLinkingRepository.GetDestinationIngredientLinkingStatus(ingSks);

            List<Ingredients> ingredients = ings.Select(i => new Ingredients
            {
                CalculationMethod = i.CalculationMethod,
                ContractStatusSk = i.ContractStatusSk,
                ContractStatus = (contractedStatus != null && contractedStatus.Count > 0) ? contractedStatus.Where(c => c.ContractStatusSk == i.ContractStatusSk).Select(c => c.ContractStatusName).SingleOrDefault() : null,
                Cost = i.SalePrice != null ? i.SalePrice.Value : 0,
                CountrySk = i.CountrySk,
                Country = (countries != null && countries.Count > 0) ? countries.Where(c => c.CountrySk == i.CountrySk).Select(c => c.CountryName).SingleOrDefault() : null,
                GenericYield = i.GenericYield,
                IngFoodGroupSk = i.FoodGroupSk,
                IngFoodGroup = (foodGroups != null && foodGroups.Count > 0) ? foodGroups.Where(c => c.FoodGroupSk == i.FoodGroupSk).Select(c => c.Name).SingleOrDefault() : null,
                IngredientsName = i.IngredientsName,
                IngSK = i.IngSk,
                MinimumOrderQuantity = i.MinimumOrderQuantity,
                PreferredSupplier = i.SupplierName,
                PrepYield = i.PrepYield,
                SpecificationDescription = i.SpecificationDescription,
                StatusSk = i.StatusSk,
                Status = (status != null && status.Count > 0) ? status.Where(c => c.StatusSk == i.StatusSk).Select(c => c.StatusName).SingleOrDefault() : null,
                Store = i.StoreSk,
                SupplierReferenceNo = i.SupplierReferenceNo,
                Weight = i.Weight,
                Barcode = i.Barcode,
                SalePrice = i.SalePrice,
                IngredientTypeId = i.IngredientTypeId,
                MearureOptionId = i.MeasureOptionId,
                IsOnMenu = i.IsOnMenu,
                IsLinkedWithOtherIng = ingredientLinkedSks != null && ingredientLinkedSks.Count > 0 ? ingredientLinkedSks.Contains(i.IngSk) : false
            }).ToList();

            return new IngredientsDetailsModel
            {
                Ingredients = ingredients,
                TotalIngredients = ingredientsDbModel.TotalIngredients
            };
        }

        public async Task<List<Ingredients>> GetIngredientDelcarationStatusModel(List<DeclarationStausDetailsModel> ings)
        {
            List<FoodGroup> foodGroups = await _ingredientsRepositoryInterface.GetFoodgroups();
            List<Country> countries = await _ingredientsRepositoryInterface.GetCountries();
            List<IngredientStatus> status = await _ingredientsRepositoryInterface.GetStatus();
            List<IngredientContractStatus> contractedStatus = await _ingredientsRepositoryInterface.GetContractedStatus();

            return ings.Select(i => new Ingredients
            {
                CalculationMethod = i.Ingredient.CalculationMethod,
                ContractStatusSk = i.Ingredient.ContractStatusSk,
                ContractStatus = (contractedStatus != null && contractedStatus.Count > 0) ? contractedStatus.Where(c => c.ContractStatusSk == i.Ingredient.ContractStatusSk).Select(c => c.ContractStatusName).SingleOrDefault() : null,
                Cost = i.Ingredient.SalePrice != null ? i.Ingredient.SalePrice.Value : 0,
                CountrySk = i.Ingredient.CountrySk,
                Country = (countries != null && countries.Count > 0) ? countries.Where(c => c.CountrySk == i.Ingredient.CountrySk).Select(c => c.CountryName).SingleOrDefault() : null,
                GenericYield = i.Ingredient.GenericYield,
                IngFoodGroupSk = i.Ingredient.FoodGroupSk,
                IngFoodGroup = (foodGroups != null && foodGroups.Count > 0) ? foodGroups.Where(c => c.FoodGroupSk == i.Ingredient.FoodGroupSk).Select(c => c.Name).SingleOrDefault() : null,
                IngredientsName = i.Ingredient.IngredientsName,
                IngSK = i.Ingredient.IngSk,
                MinimumOrderQuantity = i.Ingredient.MinimumOrderQuantity,
                PreferredSupplier = i.Ingredient.SupplierName,
                PrepYield = i.Ingredient.PrepYield,
                SpecificationDescription = i.Ingredient.SpecificationDescription,
                StatusSk = i.Ingredient.StatusSk,
                Status = (status != null && status.Count > 0) ? status.Where(c => c.StatusSk == i.Ingredient.StatusSk).Select(c => c.StatusName).SingleOrDefault() : null,
                Store = i.Ingredient.StoreSk,
                SupplierReferenceNo = i.Ingredient.SupplierReferenceNo,
                Weight = i.Ingredient.Weight,
                Barcode = i.Ingredient.Barcode,
                SalePrice = i.Ingredient.SalePrice,
                DeclarationStatus = i.DeclartaionStatusDescription
            }).ToList();
        }
    }
}
