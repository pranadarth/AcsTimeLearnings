using Athena.Application.BusinessLogic;
using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _iDishRepository;
        private readonly IDishCategoryRepository _iDishCategoryRepository;
        private readonly IDishFoodTypeRepository _iDishFoodTypeRepository;
        private readonly IDishHeatTypeRepository _iDishHeatTypeRepository;
        private readonly IDishMenuTypeRepository _iDishMenuRepository;
        private readonly IDishMealTypeRepository _iDishMealRepository;
        private readonly IDishTemplatesRepository _iDishTemplatesRepository;
        private readonly ILabelTypeRepository _iLabelTypeRepository;
        private readonly IPortionControlRepository _portionControlRepository;
        private readonly IDishProcessRepository _iDishProcessRepository;
        private readonly IDishProcessSectionRepository _iDishProcessSectionRepository;
        private readonly IDishProcessStepRepository _iDishProcessStepRepository;
        private readonly IDishTemperatureRepository _iDishTemperatureRepository;
        private readonly IDishTimeRepository _iDishTimeRepository;
        private readonly IDishPreparationRepository _iDishPreparationRepository;
        private readonly IDishSubDishRepository _iDishSubDishRepository;
        private readonly IDishIngredientsRepository _iDishIngredientsRepository;
        private readonly IDishLabelDetailsRepository _iDishLabelDetailsRepository;
        private readonly IDishCategoryMappingRepository _iDishCategoryMappingRepository;
        private readonly IIngredientsRepository _iIngredientsRepository;
        private readonly IDishIngSubstitutionRepository _dishingSubstitutionRepository;
        private readonly IDishMealMappingRepository _iDishMealMappingRepository;
        private readonly IDishMenuMappingRepository _iDishMenuMappingRepository;
        private readonly IWebHostEnvironment _env;
        private readonly DishMgmt _dishMgmt;

        public DishService(IDishRepository iDishRepository, IDishCategoryRepository iDishCategoryRepository,
            IDishFoodTypeRepository iDishFoodTypeRepository, IDishHeatTypeRepository iDishHeatTypeRepository,
            IDishMenuTypeRepository iDishMenuRepository, IDishMealTypeRepository iDishMealRepository,
            IDishTemplatesRepository iDishTemplatesRepository, ILabelTypeRepository iLabelTypeRepository,
            IPortionControlRepository portionControlRepository, IDishProcessRepository iDishProcessRepository,
            IDishProcessSectionRepository iDishProcessSectionRepository, IDishProcessStepRepository iDishProcessStepRepository,
            IDishTemperatureRepository iDishTemperatureRepository, IDishTimeRepository iDishTimeRepository,
            IDishPreparationRepository iDishPreparationRepository, IDishSubDishRepository iDishSubDishRepository,
            IDishIngredientsRepository iDishIngredientsRepository, IDishLabelDetailsRepository iDishLabelDetailsRepository,
            IDishCategoryMappingRepository iDishCategoryMappingRepository, IIngredientsRepository iIngredientsRepository,
            IDishIngSubstitutionRepository dishIngSubstitutionRepository, IDishMealMappingRepository iDishMealMappingRepository,
            IDishMenuMappingRepository iDishMenuMappingRepository, IWebHostEnvironment env)
        {
            _iDishRepository = iDishRepository;
            _iDishCategoryRepository = iDishCategoryRepository;
            _iDishFoodTypeRepository = iDishFoodTypeRepository;
            _iDishHeatTypeRepository = iDishHeatTypeRepository;
            _iDishMenuRepository = iDishMenuRepository;
            _iDishMealRepository = iDishMealRepository;
            _iDishTemplatesRepository = iDishTemplatesRepository;
            _iLabelTypeRepository = iLabelTypeRepository;
            _portionControlRepository = portionControlRepository;
            _iDishProcessRepository = iDishProcessRepository;
            _iDishProcessSectionRepository = iDishProcessSectionRepository;
            _iDishProcessStepRepository = iDishProcessStepRepository;
            _iDishTemperatureRepository = iDishTemperatureRepository;
            _iDishTimeRepository = iDishTimeRepository;
            _iDishPreparationRepository = iDishPreparationRepository;
            _iDishSubDishRepository = iDishSubDishRepository;
            _iDishIngredientsRepository = iDishIngredientsRepository;
            _iDishLabelDetailsRepository = iDishLabelDetailsRepository;
            _iDishCategoryMappingRepository = iDishCategoryMappingRepository;
            _iIngredientsRepository = iIngredientsRepository;
            _dishingSubstitutionRepository = dishIngSubstitutionRepository;
            _iDishMealMappingRepository = iDishMealMappingRepository;
            _iDishMenuMappingRepository = iDishMenuMappingRepository;
            _env = env;
            _dishMgmt = new DishMgmt(_iDishCategoryRepository, _iDishFoodTypeRepository, _iDishHeatTypeRepository, _iDishMenuRepository, _iDishMealRepository, _iDishTemplatesRepository, _iLabelTypeRepository, _portionControlRepository, _iDishProcessRepository, _iDishProcessSectionRepository, _iDishProcessStepRepository, _iDishTemperatureRepository, _iDishTimeRepository, _iDishRepository, _iDishPreparationRepository, _iDishSubDishRepository, _iDishIngredientsRepository, _iDishLabelDetailsRepository, _iDishCategoryMappingRepository, _iIngredientsRepository, _iDishMealMappingRepository, _iDishMenuMappingRepository, _env);

        }

        public async Task<DishDetailsModel> GetDishes(int recsPerPage, int currPageNo)
        {
            DishDbModel dishes = await _iDishRepository.GetDishes(recsPerPage, currPageNo);
            if (dishes == null)
                return null;

            return await _dishMgmt.GetDishDetailsModel(dishes);
        }

        public async Task<DishDetailsModel> Search(string searchText, int? dishCategoryId, float? portionSize, float? costPerPortion, int recsPerPage, int currPageNo, long? ingSk, string exactDishName)
        {
            DishDbModel dishes = new DishDbModel();
            if(!string.IsNullOrEmpty(exactDishName))
                dishes = await _iDishRepository.GetDish(exactDishName, dishCategoryId, portionSize, costPerPortion, recsPerPage, currPageNo, ingSk);
            else if (string.IsNullOrEmpty(searchText) && ingSk < 1)
                dishes = await _iDishRepository.GetDishes(recsPerPage, currPageNo);
            else
                dishes = await _iDishRepository.Search(searchText, dishCategoryId, portionSize, costPerPortion, recsPerPage, currPageNo, ingSk);

            if (dishes == null)
                return null;

            return await _dishMgmt.GetDishDetailsModel(dishes);
        }

        public async Task<object> GetMealTypes()
        {
            return await _iDishMealRepository.GetDishMealTypes();
        }

        public async Task<object> GetMenuTypes(int? locationId = null, int? sublocationId = null)
        {
            return await _iDishMenuRepository.GetDishMenuTypes(locationId, sublocationId);
        }

        public async Task<object> GetFoodTypes()
        {
            return await _iDishFoodTypeRepository.GetDishFoodTypes();
        }

        public async Task<object> GetLabelTypes()
        {
            return await _iLabelTypeRepository.GetLabelTypes();
        }

        public async Task<object> GetTemplates()
        {
            return await _iDishTemplatesRepository.GetDishTemplates();
        }

        public async Task<object> GetPortionControl()
        {
            return await _portionControlRepository.GetPortionControl();
        }

        public async Task<object> GetSpiceLevel()
        {
            return await _iDishHeatTypeRepository.GetDishHeatTypes();
        }

        public async Task<List<Dish>> GetExportDishes(GetExportDishesReqModel reqData)
        {
            List<DishManagerEntity> dishes = await _iDishRepository.GetExportDishes(reqData);
            if (dishes == null)
                return null;

            DishDbModel dishDetailsModel = new DishDbModel()
            {
                Dishes = dishes
            };

            DishDetailsModel dishDetail = await _dishMgmt.GetDishDetailsModel(dishDetailsModel);
            if (dishDetail != null)
            {
                return dishDetail.Dishes;
            }

            return null;
        }

        public async Task<DishDetailsModel> GetFilterDishes(GetFilterDishesReqModel reqData)
        {
            DishDbModel dishes = await _iDishRepository.GetFilterDishes(reqData);
            if (dishes == null)
                return null;

            return await _dishMgmt.GetDishDetailsModel(dishes);
        }

        public async Task<object> GetDishDropDowns()
        {
            return await _dishMgmt.GetDishDropDowns();
        }

        public async Task<object> GetDishCategories()
        {
            return await _iDishCategoryRepository.GetDishCategories();
        }

        public async Task<object> GetPreparationProcessTypes()
        {
            return await _iDishProcessRepository.GetPreparationProcessTypes();
        }

        public async Task<object> GetPreparationProcessSteps()
        {
            return await _iDishProcessStepRepository.GetPreparationProcessSteps();
        }

        public async Task<object> GetProcessSelectionTypes()
        {
            return await _iDishProcessSectionRepository.GetPreparationProcessSectionTypes();
        }

        public async Task<object> GetDishTimingTypes()
        {
            return await _iDishTimeRepository.GetDishTimeTypes();
        }

        public async Task<object> GetDishTemperatureUnits()
        {
            return await _iDishTemperatureRepository.GetDishTemperatureTypes();
        }

        public async Task<object> GetDishPreparationDropdowns()
        {
            return await _dishMgmt.GetDishPreparationDropdowns();
        }

        public async Task<object> SaveDish(SaveDishDetailsReqModel reqData)
        {
            return await _dishMgmt.SaveDish(reqData);
        }

        public async Task<bool> IsDishNameExists(string dishName)
        {
            return await _iDishRepository.IsDishNameExists(dishName);
        }

        public async Task<bool> UploadDishImage(string clientId, DishUploadReqModel reqData)
        {
            await _dishMgmt.UploadDishImage(clientId, reqData);

            return true;
        }

        public async Task<object> UpdateDish(UpdateDishDetailsReqModel reqData)
        {
            return await _dishMgmt.UpdateDish(reqData);
        }

        public async Task<int> GetDishSkByName(string dishName)
        {
            return await _iDishRepository.GetDishSkByName(dishName);
        }

        public async Task<DishModel> GetDishDetails(int dishSk)
        {
            return await _dishMgmt.GetDishDetails(dishSk);
        }

        public async Task<bool> SaveDishIngSubstitution(SaveDishIngSubstitutionReqModel reqData)
        {
            List<SubstituteOnDish> dishes = reqData.SubstituteOnDish.Where(s => s.PreSaleCost != s.PostSaleCost).ToList();

            await _dishingSubstitutionRepository.SaveIngredientSubstitution(reqData.PreIngSk, reqData.SubstituteWithIngSk, reqData.SubstituteOnDish, reqData.UserId);
            await _iDishIngredientsRepository.SubstituteDishIngredients(reqData.PreIngSk, reqData.SubstituteWithIngSk, reqData.SubstituteOnDish, reqData.UserId);
            await _iDishRepository.UpdateDishSalePrice(dishes, reqData.UserId);

            return true;
        }
    }
}
