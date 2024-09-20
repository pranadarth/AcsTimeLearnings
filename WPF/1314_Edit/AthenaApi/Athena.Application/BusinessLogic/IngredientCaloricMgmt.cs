using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.BusinessLogic
{
    public class IngredientCaloricMgmt
    {
        private readonly IIngredientCalorieRepository _iingredientCalorieRepository;
        private readonly IFoodCaloricTypeRepository _ifoodCaloricTypeRepository;

        public IngredientCaloricMgmt(IIngredientCalorieRepository iingredientCalorieRepository, IFoodCaloricTypeRepository ifoodCaloricTypeRepository)
        {
            _iingredientCalorieRepository = iingredientCalorieRepository;
            _ifoodCaloricTypeRepository = ifoodCaloricTypeRepository;
        }


        public async Task<List<IngredientCalorieModel>> Get(long ingSk)
        {
            List<FoodCaloricTypeEntity> foodCaloricTypes = _ifoodCaloricTypeRepository.GetAll().GetAwaiter().GetResult();
            if (foodCaloricTypes == null || foodCaloricTypes.Count < 1)
                return null;

            List<IngredientCalorieModel> ingredientCalorieDetails = new List<IngredientCalorieModel>();
            List<IngredientsMasterCaloric> ingredientCaloricInfo = await _iingredientCalorieRepository.Get(ingSk);

            foreach (FoodCaloricTypeEntity caloreType in foodCaloricTypes)
            {
                IngredientsMasterCaloric? caloricDets = null;
                if (ingredientCaloricInfo != null && ingredientCaloricInfo.Count > 0)
                {
                    caloricDets = ingredientCaloricInfo.Where(c => c.CaloricTypeSk == caloreType.CalTypeSk).SingleOrDefault();
                }

                IngredientCalorieModel ingCaloric = new IngredientCalorieModel()
                {
                    CalType = caloreType.Name,
                    CalTypeSk = caloreType.CalTypeSk,
                    Code = caloreType.Code,
                    Unit = caloreType.Unit,
                    IngMastCaloricSK = caloricDets != null ? caloricDets.IngredientMasterCaloricSk : 0,
                    IngSk = caloricDets != null ? caloricDets.IngSk : 0,
                    Value = caloricDets != null ? caloricDets.Value : null,
                };
                ingredientCalorieDetails.Add(ingCaloric);
            }

            return ingredientCalorieDetails;
        }

        public List<object> GetCaloricDetailsModel(List<IngredientCalorieModel> ings)
        {
            List<FoodCaloricTypeEntity> foodCaloricTypes = _ifoodCaloricTypeRepository.GetAll().GetAwaiter().GetResult();
            if (foodCaloricTypes == null || foodCaloricTypes.Count < 1)
                return null;

            ings = ings.Distinct().ToList();


            List<IngredientCalorieModel> distinctIngredients = ings.DistinctBy(x => x.IngSk).ToList();
            List<object> ingCalories = new List<object>();

            foreach (IngredientCalorieModel ing in distinctIngredients)
            {
                List<object> ingredientCalorieDetails = new List<object>();

                foreach (FoodCaloricTypeEntity caloreType in foodCaloricTypes)
                {
                    IngredientCalorieModel? caloricDets = null;
                    if (ings != null && ings.Count > 0)
                    {
                        caloricDets = ings.Where(c => c.CalTypeSk == caloreType.CalTypeSk && ing.IngSk == c.IngSk).SingleOrDefault();
                    }

                    IngredientCalorieModel ingCaloric = new IngredientCalorieModel()
                    {
                        CalType = caloreType.Name,
                        CalTypeSk = caloreType.CalTypeSk,
                        IngMastCaloricSK = caloricDets != null ? caloricDets.IngMastCaloricSK : 0,
                        Value = caloricDets != null ? caloricDets.Value : 0,
                        IngSk = ing.IngSk,
                        IngredientName = ing.IngredientName,
                    };
                    ingredientCalorieDetails.Add(ingCaloric);
                }

                var caloricDetails = new
                {
                    IngSk = ing.IngSk,
                    IngredientName = ing.IngredientName,
                    CaloricInfo = ingredientCalorieDetails
                };
                ingCalories.Add(caloricDetails);
            }

            return ingCalories;
        }

        public List<object> GetCaloricDetailsModelWithoutGrouping(List<IngredientCalorieModel> ings)
        {
            List<FoodCaloricTypeEntity> foodCaloricTypes = _ifoodCaloricTypeRepository.GetAll().GetAwaiter().GetResult();
            if (foodCaloricTypes == null || foodCaloricTypes.Count < 1)
                return null;

            ings = ings.Distinct().ToList();


            List<IngredientCalorieModel> distinctIngredients = ings.DistinctBy(x => x.IngSk).ToList();
            List<object> ingCalories = new List<object>();

            foreach (IngredientCalorieModel ing in distinctIngredients)
            {

                foreach (FoodCaloricTypeEntity caloreType in foodCaloricTypes)
                {
                    IngredientCalorieModel? caloricDets = null;
                    if (ings != null && ings.Count > 0)
                    {
                        caloricDets = ings.Where(c => c.CalTypeSk == caloreType.CalTypeSk && ing.IngSk == c.IngSk).SingleOrDefault();
                    }

                    IngredientCalorieModel ingCaloric = new IngredientCalorieModel()
                    {
                        CalType = caloreType.Name,
                        CalTypeSk = caloreType.CalTypeSk,
                        IngMastCaloricSK = caloricDets != null ? caloricDets.IngMastCaloricSK : 0,
                        Value = caloricDets != null ? caloricDets.Value : 0,
                        IngSk = ing.IngSk,
                        IngredientName = ing.IngredientName,
                    };
                    ingCalories.Add(ingCaloric);
                }
            }

            return ingCalories;
        }
    }
}
