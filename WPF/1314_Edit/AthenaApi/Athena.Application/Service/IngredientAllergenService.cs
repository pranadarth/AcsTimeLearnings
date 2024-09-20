using Athena.Application.BusinessLogic;
using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class IngredientAllergenService : IIngredientAllergenService
    {
        private readonly IIngredientMasterAllergenRepository _iingredientAllergenRepository;
        private readonly IAllergenRepository _iAllergenRepository;
        private readonly ISubAllergenRepository _iSubAllergenRepository;
        private readonly IIngredientAllergenOptionsRepository _iingredientAllergenOptionsRepository;

        public IngredientAllergenService(IIngredientMasterAllergenRepository iingredientAllergenRepository, IAllergenRepository iAllergenRepository, ISubAllergenRepository iSubAllergenRepository, IIngredientAllergenOptionsRepository iingredientAllergenOptionsRepository)
        {
            _iingredientAllergenRepository = iingredientAllergenRepository;
            _iAllergenRepository = iAllergenRepository;
            _iSubAllergenRepository = iSubAllergenRepository;
            _iingredientAllergenOptionsRepository = iingredientAllergenOptionsRepository;
        }

        public async Task<List<IngredientAllergenModel>> Get(long ingSk)
        {
            IngredientAllergenMgmt ingredientAllergenMgmt = new IngredientAllergenMgmt(_iingredientAllergenRepository, _iAllergenRepository, _iSubAllergenRepository);
            return await ingredientAllergenMgmt.Get(ingSk);
        }

        public async Task<object> GetAllergenOptions()
        {
            return await _iingredientAllergenOptionsRepository.GetAllergenOptions();
        }

        public async Task<bool> Save(List<SaveIngredientAllergenRequestModel> allergen)
        {
            return await _iingredientAllergenRepository.Save(allergen);
        }
    }
}
