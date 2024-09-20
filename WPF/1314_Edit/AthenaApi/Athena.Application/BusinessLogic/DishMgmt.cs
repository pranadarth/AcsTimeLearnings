using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.BusinessLogic
{
    public class DishMgmt
    {
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
        private readonly IDishRepository _iDishRepository;
        private readonly IDishPreparationRepository _iDishPreparationRepository;
        private readonly IDishSubDishRepository _iDishSubDishRepository;
        private readonly IDishIngredientsRepository _iDishIngredientsRepository;
        private readonly IDishLabelDetailsRepository _iDishLabelDetailsRepository;
        private readonly IDishCategoryMappingRepository _iDishCategoryMappingRepository;
        private readonly IIngredientsRepository _iIngredientsRepository;
        private readonly IDishMealMappingRepository _iDishMealMappingRepository;
        private readonly IDishMenuMappingRepository _iDishMenuMappingRepository;

        private readonly IWebHostEnvironment _env;

        public DishMgmt(IDishCategoryRepository iDishCategoryRepository,
            IDishFoodTypeRepository iDishFoodTypeRepository, IDishHeatTypeRepository iDishHeatTypeRepository,
            IDishMenuTypeRepository iDishMenuRepository, IDishMealTypeRepository iDishMealRepository,
            IDishTemplatesRepository iDishTemplatesRepository, ILabelTypeRepository iLabelTypeRepository,
            IPortionControlRepository portionControlRepository, IDishProcessRepository iDishProcessRepository,
            IDishProcessSectionRepository iDishProcessSectionRepository, IDishProcessStepRepository iDishProcessStepRepository,
            IDishTemperatureRepository iDishTemperatureRepository, IDishTimeRepository iDishTimeRepository, IDishRepository iDishRepository,
            IDishPreparationRepository iDishPreparationRepository, IDishSubDishRepository iDishSubDishRepository, IDishIngredientsRepository iDishIngredientsRepository,
            IDishLabelDetailsRepository iDishLabelDetailsRepository, IDishCategoryMappingRepository iDishCategoryMappingRepository,
            IIngredientsRepository iIngredientsRepository, IDishMealMappingRepository iDishMealMappingRepository,
            IDishMenuMappingRepository iDishMenuMappingRepository, IWebHostEnvironment env)
        {
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
            _iDishRepository = iDishRepository;
            _iDishPreparationRepository = iDishPreparationRepository;
            _iDishSubDishRepository = iDishSubDishRepository;
            _iDishIngredientsRepository = iDishIngredientsRepository;
            _iDishLabelDetailsRepository = iDishLabelDetailsRepository;
            _iDishCategoryMappingRepository = iDishCategoryMappingRepository;
            _iDishMealMappingRepository = iDishMealMappingRepository;
            _iIngredientsRepository = iIngredientsRepository;
            _iDishMenuMappingRepository = iDishMenuMappingRepository;
            _env = env;
        }


        public async Task<DishDetailsModel> GetDishDetailsModel(DishDbModel dishDbModel)
        {
            if (dishDbModel is null || dishDbModel.Dishes is null)
                return null;

            List<DishCategoryEntity> dishCategories = await _iDishCategoryRepository.GetDishCategories();
            List<DishFoodTypeEntity> dishFoodTypes = await _iDishFoodTypeRepository.GetDishFoodTypes();
            List<DishHeatTypeEntity> dishHeatTypes = await _iDishHeatTypeRepository.GetDishHeatTypes();
            List<MenuTypeDetailsModel> dishMenuTypes = await _iDishMenuRepository.GetDishMenuTypes();
            List<DishMealTypeEntity> dishMealTypes = await _iDishMealRepository.GetDishMealTypes();

            List<DishManagerEntity> dishes = dishDbModel.Dishes;

            List<int> dishSks = dishes.Select(d => d.DishSk).ToList();
            List<DishCategoryMappingEntity> dishCategoryMapping = await _iDishCategoryMappingRepository.GetDishCategoryMapping(dishSks);
            List<DishMealMappingEntity> dishMealTypesMapping = await _iDishMealMappingRepository.GetDishMealTypes(dishSks);
            List<DishMenuMappingEntity> dishMenuTypesMapping = await _iDishMenuMappingRepository.GetDishMenuTypes(dishSks);


            List<Dish> dish = dishes.Select(i => new Dish
            {
                ActiveStatus = i.ActiveStatus,
                Calories = i.Calories,
                CalrorieSource = i.CalrorieSource,
                CalsFromFat = i.CalsFromFat,
                Carbohydrates = i.Carbohydrates,
                CookedPortionWeight = i.CookedPortionWeight,
                CrossContamination = i.CrossContamination,
                Cost = i.Cost,
                CostPerPortion = i.CostPerPortion,
                DishCategories = dishCategoryMapping.Where(x => x.DishSk == i.DishSk).Select(x => new DishCategoryModel
                {
                    DishCategoryId = x.DishCategoryId,
                    DishCategoryCode = (dishCategories != null && dishCategories.Count > 0) ? dishCategories.Where(c => c.DishCategoryId == x.DishCategoryId).Select(c => c.DishCategoryCode).SingleOrDefault() : null,

                }).ToList(),
                DishFoodTypeId = i.DishFoodTypeId,
                DishFoodTypeCode = (dishFoodTypes != null && dishFoodTypes.Count > 0) ? dishFoodTypes.Where(c => c.DishFoodTypeId == i.DishFoodTypeId).Select(c => c.DishFoodTypeCode).SingleOrDefault() : null,
                DishSpiceLevelId = i.DishHeatTypeId,
                DishHeatTypeCode = (dishHeatTypes != null && dishHeatTypes.Count > 0) ? dishHeatTypes.Where(c => c.DishHeatTypeId == i.DishHeatTypeId).Select(c => c.DishHeatTypeCode).SingleOrDefault() : null,
                DishMenuTypes = dishMenuTypesMapping != null && dishMenuTypesMapping.Count > 0 ? dishMenuTypesMapping.Where(d => d.DishSk == i.DishSk).Select(me => new DishMenuTypeModel
                {
                    MenuTypeId = me.DishMenuTypeId != null ? me.DishMenuTypeId.Value : 0,
                    MenuType = me.DishMenuType.DishMenuTypeCode
                }).ToList() : null,

                DishMealTypes = dishMealTypesMapping != null && dishMealTypesMapping.Count > 0 ? dishMealTypesMapping.Where(d => d.DishSk == i.DishSk).Select(me => new DishMealTypeModel
                {
                    MealTypeId = me.MealTypeId != null ? me.MealTypeId.Value : 0,
                    MealType = me.DishMealType.MealTypeCode
                }).ToList() : null,
                DishName = i.DishName,
                //DishPreparation = i.DishPreparation,
                DishShelfLifeId = i.DishShelfLifeId,
                DishSk = i.DishSk,
                DishImage = i.DishImage,
                DishTempId = i.DishTempId,
                DisplayName = i.DisplayName,
                Fat = i.Fat,
                Fibre = i.Fibre,
                Popularity = i.Popularity,
                PopularitySource = i.PopularitySource,
                PortionControlId = i.PortionControlId,
                PortionSize = i.PortionSize,
                //PreparationTime = i.PreparationTime,
                Protein = i.Protein,
                SalePrice = i.SalePrice,
                Salt = i.Salt,
                SaltEquivalent = i.SaltEquivalent,
                SaturatedFat = i.SaturatedFat,
                Sodium = i.Sodium
            }).ToList();

            return new DishDetailsModel
            {
                Dishes = dish,
                TotalDishes = dishDbModel.TotalDishes
            };
        }

        public async Task<object> GetDishDropDowns()
        {
            List<DishMealTypeEntity> dishMealTypes = await _iDishMealRepository.GetDishMealTypes();
            List<MenuTypeDetailsModel> dishMenuTypes = await _iDishMenuRepository.GetDishMenuTypes();
            List<DishFoodTypeEntity> dishFoodTypes = await _iDishFoodTypeRepository.GetDishFoodTypes();
            List<DishHeatTypeEntity> dishHeatTypes = await _iDishHeatTypeRepository.GetDishHeatTypes();
            List<PortionControlEntity> portionControl = await _portionControlRepository.GetPortionControl();
            List<LabelTypeEntity> labelTypes = await _iLabelTypeRepository.GetLabelTypes();
            List<DishTemplateEntity> dishTemplates = await _iDishTemplatesRepository.GetDishTemplates();
            List<DishCategoryEntity> dishCtegories = await _iDishCategoryRepository.GetDishCategories();

            var dropdowns = new
            {
                MealTypes = dishMealTypes,
                MenuTypes = dishMenuTypes,
                FoodTypes = dishFoodTypes,
                HeatTypes = dishHeatTypes,
                PortionControl = portionControl,
                LabelTypes = labelTypes,
                DishTemplates = dishTemplates,
                DishCategories = dishCtegories
            };


            return dropdowns;
        }

        public async Task<object> GetDishPreparationDropdowns()
        {
            List<DishProcessEntity> preparationProcessTypes = await _iDishProcessRepository.GetPreparationProcessTypes();
            List<DishProcessSectionEntity> preparationProcessSectionTypes = await _iDishProcessSectionRepository.GetPreparationProcessSectionTypes();
            List<DishProcessStepEntity> preparationProcessSteps = await _iDishProcessStepRepository.GetPreparationProcessSteps();
            List<DishTemperatureEntity> dishTemperatureTypes = await _iDishTemperatureRepository.GetDishTemperatureTypes();
            List<DishTimeEntity> dishTimeTypesGetDishTimeTypes = await _iDishTimeRepository.GetDishTimeTypes();

            var dropdowns = new
            {
                PreparationProcessTypes = preparationProcessTypes,
                PreparationProcessSectionTypes = preparationProcessSectionTypes,
                PreparationProcessSteps = preparationProcessSteps,
                DishTemperatureTypes = dishTemperatureTypes,
                DishTimeTypesGetDishTimeTypes = dishTimeTypesGetDishTimeTypes,
            };


            return dropdowns;
        }

        public async Task<object> SaveDish(SaveDishDetailsReqModel reqData)
        {
            //Save Dish
            int dishSk = await _iDishRepository.SaveDish(reqData.DishGenerationInfo, reqData.PortionSize, reqData.LabourUtility, reqData.DishCost, reqData.CostPerPortion, reqData.SalePrice, reqData.CrossContamination);
            if (dishSk > 0)
            {
                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.DishLabelTypeIds != null && reqData.DishGenerationInfo.DishLabelTypeIds.Count > 0)
                    await _iDishLabelDetailsRepository.SaveDishLabelDetails(dishSk, reqData.DishGenerationInfo.DishLabelTypeIds);

                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.DishMenuTypeIds != null && reqData.DishGenerationInfo.DishMenuTypeIds.Count > 0)
                    await _iDishMenuMappingRepository.SaveDishMenuMapping(dishSk, reqData.DishGenerationInfo.DishMenuTypeIds);

                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.DishMealTypeIds != null && reqData.DishGenerationInfo.DishMealTypeIds.Count > 0)
                    await _iDishMealMappingRepository.SaveDishMealMapping(dishSk, reqData.DishGenerationInfo.DishMealTypeIds);

                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.CategoryIds != null && reqData.DishGenerationInfo.CategoryIds.Count > 0)
                    await _iDishCategoryMappingRepository.SaveDishCategoryMapping(dishSk, reqData.DishGenerationInfo.CategoryIds);

                if (reqData.DishIngredients != null && reqData.DishIngredients.Count > 0)
                    await _iDishIngredientsRepository.SaveDishIngredients(dishSk, reqData.DishIngredients);

                if (reqData.SubDishes != null && reqData.SubDishes.Count > 0)
                    await _iDishSubDishRepository.SaveSubDish(dishSk, reqData.SubDishes);

                if (reqData.DishPreparations != null && reqData.DishPreparations.Count > 0)
                    await _iDishPreparationRepository.SaveDishPreparation(dishSk, reqData.DishPreparations);
            }

            return true;
        }

        public async Task<object> UpdateDish(UpdateDishDetailsReqModel reqData)
        {
            int dishSk = reqData.DishGenerationInfo.DishSk;
            bool isSuccess = await _iDishRepository.UpdateDish(reqData.DishGenerationInfo, reqData.PortionSize, reqData.LabourUtility, reqData.DishCost, reqData.CostPerPortion, reqData.SalePrice, reqData.CrossContamination, reqData.UserId);
            if (isSuccess)
            {
                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.DishLabelTypeIds != null && reqData.DishGenerationInfo.DishLabelTypeIds.Count > 0)
                    await _iDishLabelDetailsRepository.UpdateDishLabelDetails(dishSk, reqData.DishGenerationInfo.DishLabelTypeIds);

                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.DishMenuTypeIds != null && reqData.DishGenerationInfo.DishMenuTypeIds.Count > 0)
                    await _iDishMenuMappingRepository.UpdateDishMenuMapping(dishSk, reqData.DishGenerationInfo.DishMenuTypeIds);

                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.DishMealTypeIds != null && reqData.DishGenerationInfo.DishMealTypeIds.Count > 0)
                    await _iDishMealMappingRepository.UpdateDishMealMapping(dishSk, reqData.DishGenerationInfo.DishMealTypeIds);

                if (reqData.DishGenerationInfo != null && reqData.DishGenerationInfo.CategoryIds != null && reqData.DishGenerationInfo.CategoryIds.Count > 0)
                    await _iDishCategoryMappingRepository.UpdateDishCategoryMapping(dishSk, reqData.DishGenerationInfo.CategoryIds);

                if (reqData.DishIngredients != null && reqData.DishIngredients.Count > 0)
                    await _iDishIngredientsRepository.UpdateDishIngredients(dishSk, reqData.DishIngredients, reqData.UserId);

                if (reqData.SubDishes != null && reqData.SubDishes.Count > 0)
                    await _iDishSubDishRepository.UpdateSubDish(dishSk, reqData.SubDishes, reqData.UserId);

                if (reqData.DishPreparations != null && reqData.DishPreparations.Count > 0)
                    await _iDishPreparationRepository.UpdateDishPreparation(dishSk, reqData.DishPreparations, reqData.UserId);
            }

            return true;
        }

        public async Task<bool> UploadDishImage(string clientId, DishUploadReqModel reqData)
        {
            string imageFolder = $"Images\\{clientId}\\Dishes";

            var uploadsFolder = Path.Combine(_env.ContentRootPath, imageFolder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);


            var imageFullPath = Path.Combine(uploadsFolder, reqData.ImageName);

            var imagePath = Path.Combine(imageFolder, reqData.ImageName);

            using (var fileStream = new FileStream(imageFullPath, FileMode.Create))
            {
                await reqData.Image.CopyToAsync(fileStream);
            }

            if (!string.IsNullOrEmpty(imagePath))
                imagePath = imagePath.Replace("\\", "/");

            await _iDishRepository.UpdateDishImageUrl(reqData.DishSK, imagePath);

            return true;
        }

        public async Task<DishModel> GetDishDetails(int dishSk)
        {

            DishModel dishModel = new DishModel();
            DishManagerEntity dishManagerEntity = await _iDishRepository.GetDish(dishSk);
            if (dishManagerEntity == null)
                throw new Exception("Dish not found");

            dishModel.CostPerPortion = dishManagerEntity.CostPerPortion;
            dishModel.DishCost = dishManagerEntity.Cost;
            dishModel.CrossContamination = dishManagerEntity.CrossContamination;
            dishModel.SalePrice = dishManagerEntity.SalePrice;
            dishModel.LabourUtility = dishManagerEntity.UtilityCost;
            dishModel.PortionSize = dishManagerEntity.PortionSize;

            List<DishCategoryEntity> categories = await _iDishCategoryRepository.GetDishCategories();
            List<DishCategoryMappingEntity> dishCategoryMapping = await _iDishCategoryMappingRepository.GetDishCategoryMapping(dishSk);
            List<DishMealMappingEntity> dishMealTypesMapping = await _iDishMealMappingRepository.GetDishMealTypes(dishSk);
            List<DishMenuMappingEntity> dishMenuTypesMapping = await _iDishMenuMappingRepository.GetDishMenuTypes(dishSk);

            dishModel.DishGenerationInfo = new DishGenerationInfoModel()
            {
                ActiveStaus = dishManagerEntity.ActiveStatus,
                Calories = dishManagerEntity.Calories,
                CalrorieSource = dishManagerEntity.CalrorieSource,
                CalsFromFat = dishManagerEntity.CalsFromFat,
                Carbohydrates = dishManagerEntity.Carbohydrates,
                CookedPortionWeight = dishManagerEntity.CookedPortionWeight,
                DishCategories = dishCategoryMapping.Select(x => new DishCategoryModel
                {
                    DishCategoryId = x.DishCategoryId,
                    DishCategoryCode = categories != null && categories.Count > 0 ? categories.Where(c => c.DishCategoryId == x.DishCategoryId).Select(c => c.DishCategoryCode).SingleOrDefault() : null,

                }).ToList(),
                DishFoodTypeId = dishManagerEntity.DishFoodTypeId,
                DishSpiceLevelId = dishManagerEntity.DishHeatTypeId,
                DishMenuTypes = dishMenuTypesMapping != null && dishMenuTypesMapping.Count > 0 ? dishMenuTypesMapping.Select(me => new DishMenuTypeModel
                {
                    MenuTypeId = me.DishMenuTypeId != null ? me.DishMenuTypeId.Value : 0,
                    MenuType = me.DishMenuType.DishMenuTypeCode
                }).ToList() : null,

                DishMealTypes = dishMealTypesMapping != null && dishMealTypesMapping.Count > 0 ? dishMealTypesMapping.Select(me => new DishMealTypeModel
                {
                    MealTypeId = me.MealTypeId != null ? me.MealTypeId.Value : 0,
                    MealType = me.DishMealType.MealTypeCode
                }).ToList() : null,

                DishName = dishManagerEntity.DishName,
                DishSk = dishManagerEntity.DishSk,
                DishImage = dishManagerEntity.DishImage,
                DishTempId = dishManagerEntity.DishTempId,
                DisplayName = dishManagerEntity.DisplayName,
                Fat = dishManagerEntity.Fat,
                Fibre = dishManagerEntity.Fibre,
                Popularity = dishManagerEntity.Popularity,
                PopularitySource = dishManagerEntity.PopularitySource,
                PortionControlId = dishManagerEntity.PortionControlId,
                Protein = dishManagerEntity.Protein,
                Salt = dishManagerEntity.Salt,
                SaltEquivalent = dishManagerEntity.SaltEquivalent,
                SaturatedFat = dishManagerEntity.SaturatedFat,
                Sodium = dishManagerEntity.Sodium
            };

            List<DishIngredientEntity> dishIngredients = await _iDishIngredientsRepository.GetDishIngredients(dishSk);
            if (dishIngredients != null && dishIngredients.Count > 0)
            {
                List<long> ingSks = dishIngredients.Select(i => i.IngSk).ToList();
                List<IngredientsMaster> ingredientDetails = await _iIngredientsRepository.GetIngredients(ingSks);
                List<Supplier> supplier = await _iIngredientsRepository.GetSuppliers();

                dishModel.DishIngredients = dishIngredients.Select(i => new DishIngredientModel
                {
                    DishIngSk = i.DishIngSk,
                    DishSk = i.DishSk,
                    IngredientName = ingredientDetails != null ? ingredientDetails.Where(x => x.IngSk == i.IngSk).Select(i => i.IngredientsName).SingleOrDefault() : null,
                    IngSk = i.IngSk,
                    SupplierId = i.SupplierId,
                    SupplierName = supplier != null ? supplier.Where(x => x.SupplierId == i.SupplierId).Select(i => i.Name).SingleOrDefault() : null,
                    Quantity = i.Quantity,
                    MeasureOptionId = i.MeasureOptionId,
                    Weight = i.Weight,
                    UnitCost = i.UnitCost,
                    CostExtension = i.CostExtension
                }).ToList();
            }

            List<DishSubDishEntity> subDishes = await _iDishSubDishRepository.GetSubDishes(dishSk);
            if (subDishes != null && subDishes.Count > 0)
            {
                List<int> subDishSks = subDishes.Select(d => d.DishSubDishSk).ToList();
                List<DishManagerEntity> subDishDetails = await _iDishRepository.GetDishes(subDishSks);
                List<DishCategoryMappingEntity> subDishCategoryMapping = await _iDishCategoryMappingRepository.GetDishCategoryMapping(subDishSks);

                dishModel.SubDishes = subDishes.Select(s => new SubDishesModel
                {
                    DishSk = s.SubDishSk,
                    DishName = subDishDetails != null && subDishDetails.Count > 0 ? subDishDetails.Where(x => x.DishSk == s.DishSk).Select(x => x.DishName).SingleOrDefault() : null,
                    DisplayName = subDishDetails != null && subDishDetails.Count > 0 ? subDishDetails.Where(x => x.DishSk == s.DishSk).Select(x => x.DisplayName).SingleOrDefault() : null,
                    DishCategories = dishCategoryMapping.Where(x => x.DishSk == s.DishSk).Select(x => new DishCategoryModel
                    {
                        DishCategoryId = x.DishCategoryId,
                        DishCategoryCode = (categories != null && categories.Count > 0) ? categories.Where(c => c.DishCategoryId == x.DishCategoryId).Select(c => c.DishCategoryCode).SingleOrDefault() : null,

                    }).ToList(),
                    SubDishSk = s.SubDishSk,
                    Quantity = s.Quantity,
                    Units = s.Units,
                    Cost = s.Cost
                }).ToList();
            }

            List<DishPreparationEntity> dishPreparations = await _iDishPreparationRepository.GetDishPreparations(dishSk);
            if (dishPreparations != null && dishPreparations.Count > 0)
            {
                List<DishProcessEntity> preparationProcessTypes = await _iDishProcessRepository.GetPreparationProcessTypes();
                List<DishProcessSectionEntity> preparationProcessSectionTypes = await _iDishProcessSectionRepository.GetPreparationProcessSectionTypes();
                List<DishProcessStepEntity> preparationProcessSteps = await _iDishProcessStepRepository.GetPreparationProcessSteps();
                List<DishTemperatureEntity> dishTemperatureTypes = await _iDishTemperatureRepository.GetDishTemperatureTypes();
                List<DishTimeEntity> dishTimeTypes = await _iDishTimeRepository.GetDishTimeTypes();

                dishModel.DishPreparations = dishPreparations.Select(p => new DishPreparationsModel
                {
                    DishPrepSk = p.DishPrepSk,
                    DishSk = p.DishSk,
                    DishPrepStepSequence = p.DishPrepStepSequence,
                    DishPrepMethod = p.DishPrepMethod,
                    DishProcessesSk = p.DishProcessesSk,
                    DishProcesses = preparationProcessTypes.Any() ? preparationProcessTypes.Where(x => x.DishProcessesSk == p.DishProcessesSk).Select(x => x.ProcessCode).SingleOrDefault() : null,
                    DishProcStepSk = p.DishProcStepSk,
                    DishProcStep = preparationProcessSteps.Any() ? preparationProcessSteps.Where(x => x.DishProcStepSk == p.DishProcStepSk).Select(x => x.ProcessStepCode).SingleOrDefault() : null,
                    DishProcSectionSk = p.DishProcSectionSk,
                    DishProcSection = preparationProcessSectionTypes.Any() ? preparationProcessSectionTypes.Where(x => x.DishProcSectionSk == p.DishProcSectionSk).Select(x => x.ProcessSectionCode).SingleOrDefault() : null,
                    DishTimeSk = p.DishTimeSk,
                    DishTime = dishTimeTypes.Any() ? dishTimeTypes.Where(x => x.DishTimeSk == p.DishTimeSk).Select(x => x.DishTimeCode).SingleOrDefault() : null,
                    DishPrepTime = p.DishPrepTime,
                    DishTempSk = p.DishTempSk,
                    DishTemp = dishTemperatureTypes.Any() ? dishTemperatureTypes.Where(x => x.DishTempSk == p.DishTempSk).Select(x => x.DishTempCode).SingleOrDefault() : null,
                    DishLowTemp = p.DishLowTemp,
                    DishHighTemp = p.DishHighTemp,
                    DishHaccpFlag = p.DishHaccpFlag
                }).ToList();
            }
            return dishModel;
        }
    }
}
