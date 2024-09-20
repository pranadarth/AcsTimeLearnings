using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IDishService
    {
        public Task<DishDetailsModel> GetDishes(int recsPerPage, int currPageNo);

        public Task<DishModel> GetDishDetails(int dishSk);
        public Task<DishDetailsModel> Search(string dishName, int? dishCategoryId, float? portionSize, float? costPerPortion, int recsPerPage, int currPageNo, long? ingSk, string exactDishName);

        public Task<object> GetMealTypes();
        public Task<object> GetMenuTypes(int? locationId = null, int? sublocationId = null);
        public Task<object> GetFoodTypes();
        public Task<object> GetLabelTypes();
        public Task<object> GetTemplates();
        public Task<object> GetPortionControl();
        public Task<object> GetSpiceLevel();
        public Task<object> GetDishDropDowns();
        public Task<object> GetDishCategories();

        public Task<object> GetPreparationProcessTypes();
        public Task<object> GetPreparationProcessSteps();
        public Task<object> GetProcessSelectionTypes();
        public Task<object> GetDishTimingTypes();
        public Task<object> GetDishTemperatureUnits();

        public Task<object> GetDishPreparationDropdowns();

        public Task<List<Dish>> GetExportDishes(GetExportDishesReqModel reqData);
        public Task<DishDetailsModel> GetFilterDishes(GetFilterDishesReqModel reqData);
        public Task<object> SaveDish(SaveDishDetailsReqModel reqData);
        public Task<bool> IsDishNameExists(string dishName);
        public Task<int> GetDishSkByName(string dishName);
        public Task<bool> UploadDishImage(string clientId, DishUploadReqModel reqData);
        public Task<object> UpdateDish(UpdateDishDetailsReqModel reqData);

        public Task<bool> SaveDishIngSubstitution(SaveDishIngSubstitutionReqModel reqData);

    }
}
