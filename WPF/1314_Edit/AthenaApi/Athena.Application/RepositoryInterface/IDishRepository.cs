using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishRepository
    {
        public Task<DishDbModel> GetDishes(int recsPerPage, int currPageNo);
        public Task<DishDbModel> Search(string searchText, int? dishCategoryId, float? portionSize, float? costPerPortion, int recsPerPage, int currPageNo, long? ingSk);
        public Task<List<DishManagerEntity>> GetExportDishes(GetExportDishesReqModel reqData);
        public Task<DishDbModel> GetFilterDishes(GetFilterDishesReqModel reqData);
        public Task<int> SaveDish(DishGenerationInfoReqModel dishGenerationInfoModel, float? portionSize, float? utilityCost, float? cost, float? costPerPortion,
                                         float? salePrice, bool crossContamination);

        public Task<bool> UpdateDish(DishGenerationInfoReqModel dishGenerationInfoModel, float? portionSize, float? utilityCost, float? cost, float? costPerPortion,
                                        float? salePrice, bool crossContamination, string userId);
        public Task<bool> IsDishNameExists(string dishName);

        public Task<bool> UpdateDishImageUrl(int dishSk, string dishImageUrl);

        public Task<int> GetDishSkByName(string dishName);
        public Task<DishManagerEntity> GetDish(int dishSk);
        public Task<List<DishManagerEntity>> GetDishes(List<int> dishSks);
        public Task<bool> UpdateDishSalePrice(List<SubstituteOnDish> dishesToUpdateCost, string userId);

        public Task<DishDbModel> GetDish(string exactDishName, int? dishCategoryId, float? portionSize, float? costPerPortion, int recsPerPage, int currPageNo, long? ingSk);

    }
}
