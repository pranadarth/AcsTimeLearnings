using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Athena.Infrastructure.Repository
{
    public class DishRepository : IDishRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishRepository> _logger;

        public DishRepository(ILogger<DishRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<DishDbModel> GetDishes(int recsPerPage, int currPageNo)
        {
            int skip = recsPerPage * currPageNo;
            List<DishManagerEntity> dishes = await _athenaDbcontext.DishManagerEntity.Skip(skip)
                                                      .Take(recsPerPage).ToListAsync();

            return new DishDbModel
            {
                Dishes = dishes,
                TotalDishes = await _athenaDbcontext.DishManagerEntity.CountAsync()
            };
        }

        public async Task<DishDbModel> Search(string dishName, int? dishCategoryId, float? portionSize, float? costPerPortion, int recsPerPage, int currPageNo, long? ingSk)
        {
            //TODO optimize
            if (ingSk == null || ingSk < 1)
            {
                var initialQuery = from dm in _athenaDbcontext.DishManagerEntity
                                   select new
                                   {
                                       DishManager = dm,
                                       DishCategoryMapping = (DishCategoryMappingEntity)null
                                   };

                if (dishCategoryId != null)
                {
                    initialQuery = from dm in initialQuery
                                   join catMap in _athenaDbcontext.DishCategoryMappingEntity
                                   on dm.DishManager.DishSk equals catMap.DishSk
                                   select new
                                   {
                                       DishManager = dm.DishManager,
                                       DishCategoryMapping = catMap
                                   };
                }

                var query = initialQuery.AsQueryable();

                if (!string.IsNullOrEmpty(dishName))
                    query = query.Where(i => i.DishManager.DishName.Contains(dishName));

                if (dishCategoryId != null)
                    query = query.Where(i => i.DishCategoryMapping.DishCategoryId == dishCategoryId);

                if (portionSize != null)
                    query = query.Where(i => i.DishManager.PortionSize == portionSize);

                if (costPerPortion != null)
                    query = query.Where(i => i.DishManager.CostPerPortion == costPerPortion);

                int skip = recsPerPage * currPageNo;

                List<DishManagerEntity> dishes = await query.Skip(skip).Take(recsPerPage).Select(d => d.DishManager).ToListAsync();

                return new DishDbModel
                {
                    Dishes = dishes,
                    TotalDishes = await query.CountAsync()
                };
            }
            else
            {
                var query = (from dm in _athenaDbcontext.DishManagerEntity.AsQueryable()
                             join di in _athenaDbcontext.DishIngredientEntity
                             on dm.DishSk equals di.DishSk
                             where di.IngSk == ingSk
                             select dm).ToList();


                if (!string.IsNullOrEmpty(dishName))
                    query.Where(i => i.DishName.Contains(dishName));

                int skip = recsPerPage * currPageNo;

                List<DishManagerEntity> dishes = query.Skip(skip).Take(recsPerPage).ToList();

                return new DishDbModel
                {
                    Dishes = dishes,
                    TotalDishes = query.Count()
                };
            }
        }

        public async Task<DishDbModel> GetDish(string exactDishName, int? dishCategoryId, float? portionSize, float? costPerPortion, int recsPerPage, int currPageNo, long? ingSk) 
        {
            if (ingSk == null || ingSk < 1)
            {
                var initialQuery = from dm in _athenaDbcontext.DishManagerEntity
                                   select new
                                   {
                                       DishManager = dm,
                                       DishCategoryMapping = (DishCategoryMappingEntity)null
                                   };

                if (dishCategoryId != null)
                {
                    initialQuery = from dm in initialQuery
                                   join catMap in _athenaDbcontext.DishCategoryMappingEntity
                                   on dm.DishManager.DishSk equals catMap.DishSk
                                   select new
                                   {
                                       DishManager = dm.DishManager,
                                       DishCategoryMapping = catMap
                                   };
                }

                var query = initialQuery.AsQueryable();

               
                    query = query.Where(i => i.DishManager.DishName == exactDishName);

                if (dishCategoryId != null)
                    query = query.Where(i => i.DishCategoryMapping.DishCategoryId == dishCategoryId);

                if (portionSize != null)
                    query = query.Where(i => i.DishManager.PortionSize == portionSize);

                if (costPerPortion != null)
                    query = query.Where(i => i.DishManager.CostPerPortion == costPerPortion);

                int skip = recsPerPage * currPageNo;

                List<DishManagerEntity> dishes = await query.Skip(skip).Take(recsPerPage).Select(d => d.DishManager).ToListAsync();

                return new DishDbModel
                {
                    Dishes = dishes,
                    TotalDishes = await query.CountAsync()
                };
            }
            else
            {
                var query = (from dm in _athenaDbcontext.DishManagerEntity.AsQueryable()
                             join di in _athenaDbcontext.DishIngredientEntity
                             on dm.DishSk equals di.DishSk
                             where di.IngSk == ingSk
                             select dm).ToList();


                if (!string.IsNullOrEmpty(exactDishName))
                    query.Where(i => i.DishName == exactDishName);

                int skip = recsPerPage * currPageNo;

                List<DishManagerEntity> dishes = query.Skip(skip).Take(recsPerPage).ToList();

                return new DishDbModel
                {
                    Dishes = dishes,
                    TotalDishes = query.Count()
                };
            }
        }


        public async Task<List<DishManagerEntity>> GetExportDishes(GetExportDishesReqModel reqData)
        {
            var initialQuery = from dm in _athenaDbcontext.DishManagerEntity
                               select new
                               {
                                   DishManager = dm,
                                   DishLabelDetails = (DishLabelDetailsEntity)null,
                                   DishMenuMapping = (DishMenuMappingEntity)null,
                                   DishMealMapping = (DishMealMappingEntity)null
                               };

            if (reqData.MealTypeIds != null)
            {
                initialQuery = from dm in initialQuery
                               join meal in _athenaDbcontext.DishMealMappingEntity
                               on dm.DishManager.DishSk equals meal.DishSk
                               select new
                               {
                                   DishManager = dm.DishManager,
                                   DishLabelDetails = dm.DishLabelDetails,
                                   DishMenuMapping = dm.DishMenuMapping,
                                   DishMealMapping = meal
                               };
            }

            if (reqData.MenuTypeIds != null)
            {
                initialQuery = from dm in initialQuery
                               join menu in _athenaDbcontext.DishMenuMappingEntity
                               on dm.DishManager.DishSk equals menu.DishSk
                               select new
                               {
                                   DishManager = dm.DishManager,
                                   DishLabelDetails = dm.DishLabelDetails,
                                   DishMenuMapping = menu,
                                   DishMealMapping = dm.DishMealMapping
                               };
            }

            if (reqData.LabelTypeIds != null)
            {
                initialQuery = from dm in initialQuery
                               join label in _athenaDbcontext.DishLabelDetailsEntity
                               on dm.DishManager.DishSk equals label.DishSk
                               select new
                               {
                                   DishManager = dm.DishManager,
                                   DishLabelDetails = label,
                                   DishMenuMapping = dm.DishMenuMapping,
                                   DishMealMapping = dm.DishMealMapping
                               };
            }


            var query = initialQuery.AsQueryable();


            if (reqData.MealTypeIds != null && reqData.MealTypeIds.Count > 0)
                query = query.Where(x => x.DishMealMapping != null && x.DishMealMapping.MealTypeId != null && reqData.MealTypeIds.Contains(x.DishMealMapping.MealTypeId.Value));

            if (reqData.MenuTypeIds != null && reqData.MenuTypeIds.Count > 0)
                query = query.Where(x => x.DishMenuMapping != null && x.DishMenuMapping.DishMenuTypeId != null && reqData.MenuTypeIds.Contains(x.DishMenuMapping.DishMenuTypeId.Value));

            if (reqData.FoodTypeIds != null && reqData.FoodTypeIds.Count > 0)
                query = query.Where(x => x.DishManager.DishFoodTypeId != null && reqData.FoodTypeIds.Contains(x.DishManager.DishFoodTypeId.Value));

            if (reqData.LabelTypeIds != null && reqData.LabelTypeIds.Count > 0)
                query = query.Where(x => x.DishLabelDetails != null && reqData.LabelTypeIds.Contains(x.DishLabelDetails.LabelTypeId));

            if (reqData.TemplateIds != null && reqData.TemplateIds.Count > 0)
                query = query.Where(x => x.DishManager.DishTempId != null && reqData.TemplateIds.Contains(x.DishManager.DishTempId.Value));

            if (reqData.PortionControlIds != null && reqData.PortionControlIds.Count > 0)
                query = query.Where(x => x.DishManager.PortionControlId != null && reqData.PortionControlIds.Contains(x.DishManager.PortionControlId.Value));

            if (reqData.SpiceLevelIds != null && reqData.SpiceLevelIds.Count > 0)
                query = query.Where(x => x.DishManager.DishHeatTypeId != null && reqData.SpiceLevelIds.Contains(x.DishManager.DishHeatTypeId.Value));

            return await query.Select(x => x.DishManager).ToListAsync();
        }

        public async Task<DishDbModel> GetFilterDishes(GetFilterDishesReqModel reqData)
        {
            var initialQuery = from dm in _athenaDbcontext.DishManagerEntity
                               select new
                               {
                                   DishManager = dm,
                                   DishLabelDetails = (DishLabelDetailsEntity)null,
                                   DishMenuMapping = (DishMenuMappingEntity)null,
                                   DishMealMapping = (DishMealMappingEntity)null,
                                   DishCategoryMapping = (DishCategoryMappingEntity)null
                               };

            if (reqData.MealTypeIds != null)
            {
                initialQuery = from dm in initialQuery
                               join meal in _athenaDbcontext.DishMealMappingEntity
                               on dm.DishManager.DishSk equals meal.DishSk
                               select new
                               {
                                   DishManager = dm.DishManager,
                                   DishLabelDetails = dm.DishLabelDetails,
                                   DishMenuMapping = dm.DishMenuMapping,
                                   DishMealMapping = meal,
                                   DishCategoryMapping = dm.DishCategoryMapping
                               };
            }

            if (reqData.MenuTypeIds != null)
            {
                initialQuery = from dm in initialQuery
                               join menu in _athenaDbcontext.DishMenuMappingEntity
                               on dm.DishManager.DishSk equals menu.DishSk
                               select new
                               {
                                   DishManager = dm.DishManager,
                                   DishLabelDetails = dm.DishLabelDetails,
                                   DishMenuMapping = menu,
                                   DishMealMapping = dm.DishMealMapping,
                                   DishCategoryMapping = dm.DishCategoryMapping
                               };
            }

            if (reqData.LabelTypeIds != null)
            {
                initialQuery = from dm in initialQuery
                               join label in _athenaDbcontext.DishLabelDetailsEntity
                               on dm.DishManager.DishSk equals label.DishSk
                               select new
                               {
                                   DishManager = dm.DishManager,
                                   DishLabelDetails = label,
                                   DishMenuMapping = dm.DishMenuMapping,
                                   DishMealMapping = dm.DishMealMapping,
                                   DishCategoryMapping = dm.DishCategoryMapping
                               };
            }

            if (reqData.CategoryIds != null)
            {
                initialQuery = from dm in initialQuery
                               join catMap in _athenaDbcontext.DishCategoryMappingEntity
                               on dm.DishManager.DishSk equals catMap.DishSk
                               select new
                               {
                                   DishManager = dm.DishManager,
                                   DishLabelDetails = dm.DishLabelDetails,
                                   DishMenuMapping = dm.DishMenuMapping,
                                   DishMealMapping = dm.DishMealMapping,
                                   DishCategoryMapping = catMap
                               };
            }

            var query = initialQuery.AsQueryable();

            if (reqData.CategoryIds != null && reqData.CategoryIds.Count > 0)
                query = query.Where(x => reqData.CategoryIds.Contains(x.DishCategoryMapping.DishCategoryId));

            if (reqData.PortionSize != null && reqData.PortionSize.Count > 0)
                query = query.Where(x => x.DishManager.PortionSize != null && reqData.PortionSize.Contains(x.DishManager.PortionSize.Value));

            if (reqData.CostPerDish != null && reqData.CostPerDish.Count > 0)
                query = query.Where(x => x.DishManager.Cost != null && reqData.CostPerDish.Contains(x.DishManager.Cost.Value));

            if (reqData.SpiceLevelIds != null && reqData.SpiceLevelIds.Count > 0)
                query = query.Where(x => x.DishManager.DishHeatTypeId != null && reqData.SpiceLevelIds.Contains(x.DishManager.DishHeatTypeId.Value));

            if (reqData.MealTypeIds != null && reqData.MealTypeIds.Count > 0)
                query = query.Where(x => x.DishMealMapping != null && x.DishMealMapping.MealTypeId != null && reqData.MealTypeIds.Contains(x.DishMealMapping.MealTypeId.Value));

            if (reqData.MenuTypeIds != null && reqData.MenuTypeIds.Count > 0)
                query = query.Where(x => x.DishMenuMapping != null && x.DishMenuMapping.DishMenuTypeId != null && reqData.MenuTypeIds.Contains(x.DishMenuMapping.DishMenuTypeId.Value));

            if (reqData.FoodTypeIds != null && reqData.FoodTypeIds.Count > 0)
                query = query.Where(x => x.DishManager.DishFoodTypeId != null && reqData.FoodTypeIds.Contains(x.DishManager.DishFoodTypeId.Value));

            if (reqData.LabelTypeIds != null && reqData.LabelTypeIds.Count > 0)
                query = query.Where(x => x.DishLabelDetails != null && reqData.LabelTypeIds.Contains(x.DishLabelDetails.LabelTypeId));

            List<DishManagerEntity> dishes = await query.Select(x => x.DishManager).ToListAsync();

            return new DishDbModel
            {
                Dishes = dishes,
                TotalDishes = await query.CountAsync()
            };
        }

        public async Task<int> SaveDish(DishGenerationInfoReqModel reqData, float? portionSize, float? utilityCost, float? cost, float? costPerPortion,
                                         float? salePrice, bool crossContamination)
        {
            DishManagerEntity newDish = new DishManagerEntity()
            {
                DishName = reqData.DishName,
                DisplayName = reqData.DisplayName,
                DishDescription = reqData.Description,
                DishFoodTypeId = reqData.DishFoodTypeId,
                DishHeatTypeId = reqData.DishSpiceLevelId,
                PortionControlId = reqData.PortionControlId,
                DishTempId = reqData.DishTempId,
                ActiveStatus = reqData.ActiveStaus,
                PortionSize = portionSize,
                UtilityCost = utilityCost,
                Cost = cost,
                CostPerPortion = costPerPortion,
                SalePrice = salePrice,
                CookedPortionWeight = reqData.CookedPortionWeight,
                CrossContamination = crossContamination,
                Calories = reqData.Calories,
                Protein = reqData.Protein,
                Salt = reqData.Salt,
                Fat = reqData.Fat,
                Fibre = reqData.Fibre,
                Sodium = reqData.Sodium,
                Carbohydrates = reqData.Carbohydrates,
                Popularity = reqData.Popularity,
                CalsFromFat = reqData.CalsFromFat,
                CalrorieSource = reqData.CalrorieSource,
                SaltEquivalent = reqData.SaltEquivalent,
                SaturatedFat = reqData.SaturatedFat,
                PopularitySource = reqData.PopularitySource
                //CreatedBy = reqData.UserId,
                //CreatedDate = DateTime.UtcNow
            };

            await _athenaDbcontext.DishManagerEntity.AddAsync(newDish);
            await _athenaDbcontext.SaveChangesAsync();

            return newDish.DishSk;
        }

        public async Task<bool> IsDishNameExists(string dishName)
        {
            return await _athenaDbcontext.DishManagerEntity.Where(d => d.DishName == dishName).Select(x => true).SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateDishImageUrl(int dishkSk, string imageUrl)
        {
            DishManagerEntity? dishDetails = await _athenaDbcontext.DishManagerEntity.Where(x => x.DishSk == dishkSk).SingleOrDefaultAsync();
            if (dishDetails != null)
            {
                dishDetails.DishImage = imageUrl;
            }
            await _athenaDbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateDish(DishGenerationInfoReqModel reqData, float? portionSize, float? utilityCost, float? cost, float? costPerPortion, float? salePrice, bool crossContamination, string userId)
        {
            DishManagerEntity? dishDetails = await _athenaDbcontext.DishManagerEntity.Where(x => x.DishSk == reqData.DishSk).SingleOrDefaultAsync();
            if (dishDetails == null)
                throw new Exception("Dish not found");

            dishDetails.DishName = reqData.DishName;
            dishDetails.DisplayName = reqData.DisplayName;
            dishDetails.DishDescription = reqData.Description;
            dishDetails.DishFoodTypeId = reqData.DishFoodTypeId;
            dishDetails.DishHeatTypeId = reqData.DishSpiceLevelId;
            dishDetails.PortionControlId = reqData.PortionControlId;
            dishDetails.DishTempId = reqData.DishTempId;
            dishDetails.ActiveStatus = reqData.ActiveStaus;
            dishDetails.PortionSize = portionSize;
            dishDetails.UtilityCost = utilityCost;
            dishDetails.Cost = cost;
            dishDetails.CostPerPortion = costPerPortion;
            dishDetails.SalePrice = salePrice;
            dishDetails.CookedPortionWeight = reqData.CookedPortionWeight;
            dishDetails.CrossContamination = crossContamination;
            dishDetails.Calories = reqData.Calories;
            dishDetails.Protein = reqData.Protein;
            dishDetails.Salt = reqData.Salt;
            dishDetails.Fat = reqData.Fat;
            dishDetails.Fibre = reqData.Fibre;
            dishDetails.Sodium = reqData.Sodium;
            dishDetails.Carbohydrates = reqData.Carbohydrates;
            dishDetails.Popularity = reqData.Popularity;
            dishDetails.CalsFromFat = reqData.CalsFromFat;
            dishDetails.CalrorieSource = reqData.CalrorieSource;
            dishDetails.SaltEquivalent = reqData.SaltEquivalent;
            dishDetails.SaturatedFat = reqData.SaturatedFat;
            dishDetails.PopularitySource = reqData.PopularitySource;
            dishDetails.ModifiedBy = userId;
            dishDetails.ModifiedDate = DateTime.UtcNow;

            await _athenaDbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetDishSkByName(string dishName)
        {
            return await _athenaDbcontext.DishManagerEntity.Where(d => d.DishName == dishName).Select(d => d.DishSk).SingleOrDefaultAsync();
        }

        public async Task<DishManagerEntity?> GetDish(int dishSk)
        {
            return await _athenaDbcontext.DishManagerEntity.Where(d => d.DishSk == dishSk).SingleOrDefaultAsync();
        }

        public async Task<List<DishManagerEntity>?> GetDishes(List<int> dishSks)
        {
            return await _athenaDbcontext.DishManagerEntity.Where(d => dishSks.Contains(d.DishSk)).ToListAsync();
        }

        public async Task<bool> UpdateDishSalePrice(List<SubstituteOnDish> dishesToUpdateCost, string userId)
        {
            List<int> dishSks = dishesToUpdateCost.Select(d => d.DishSk).ToList();

            List<DishManagerEntity> dishes = await _athenaDbcontext.DishManagerEntity.Where(d => dishSks.Contains(d.DishSk)).ToListAsync();

            if (dishes != null && dishes.Count > 0)
            {
                foreach (DishManagerEntity dish in dishes)
                {
                    SubstituteOnDish? dishDet = dishesToUpdateCost.Where(d => d.DishSk == dish.DishSk).SingleOrDefault();
                    if (dishDet != null)
                    {
                        dish.SalePrice = dishDet.PostSaleCost;
                        dish.ModifiedBy = userId;
                        dish.ModifiedDate = DateTime.UtcNow;
                    }
                }
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }
    }
}
