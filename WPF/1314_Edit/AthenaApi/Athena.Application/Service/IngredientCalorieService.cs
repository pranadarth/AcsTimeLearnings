using Athena.Application.BusinessLogic;
using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class IngredientCalorieService : IIngredientCalorieService
    {
        private readonly IIngredientCalorieRepository _iingredientCalorieRepository;
        private readonly IFoodCaloricTypeRepository _ifoodCaloricTypeRepository;

        public IngredientCalorieService(IIngredientCalorieRepository iingredientCalorieRepository, IFoodCaloricTypeRepository ifoodCaloricTypeRepository)
        {
            _iingredientCalorieRepository = iingredientCalorieRepository;
            _ifoodCaloricTypeRepository = ifoodCaloricTypeRepository;
        }

        public async Task<List<IngredientCalorieModel>> Get(long ingSk)
        {
            IngredientCaloricMgmt ingredientCaloricMgmt = new IngredientCaloricMgmt(_iingredientCalorieRepository, _ifoodCaloricTypeRepository);

            return await ingredientCaloricMgmt.Get(ingSk);
        }

        public async Task<bool> Save(List<SaveIngredientCaloricReqModel> caloric)
        {
            return await _iingredientCalorieRepository.Save(caloric);
        }

        public async Task<bool> Update(List<UpdateIngredientCaloricInfoReqModel> caloric)
        {
            return await _iingredientCalorieRepository.Update(caloric);
        }
    }
}
