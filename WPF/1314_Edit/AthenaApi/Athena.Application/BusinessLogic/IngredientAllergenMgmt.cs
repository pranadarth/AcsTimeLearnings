using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.BusinessLogic
{
    public class IngredientAllergenMgmt
    {
        private readonly IIngredientMasterAllergenRepository _iingredientAllergenRepository;
        private readonly IAllergenRepository _iAllergenRepository;
        private readonly ISubAllergenRepository _iSubAllergenRepository;

        public IngredientAllergenMgmt(IIngredientMasterAllergenRepository iingredientAllergenRepository, IAllergenRepository iAllergenRepository, ISubAllergenRepository iSubAllergenRepository)
        {
            _iingredientAllergenRepository = iingredientAllergenRepository;
            _iAllergenRepository = iAllergenRepository;
            _iSubAllergenRepository = iSubAllergenRepository;
        }
        public async Task<List<IngredientAllergenModel>> Get(long ingSk)
        {
            List<AllergenEntity> allergens = _iAllergenRepository.GetAll().GetAwaiter().GetResult();
            if (allergens == null || allergens.Count < 1)
                return null;

            List<SubAllergensEntity> suballergens = _iSubAllergenRepository.GetAll().GetAwaiter().GetResult();
            if (suballergens == null || suballergens.Count < 1)
                return null;

            List<IngredientAllergenModel> ingredientMasterAllergens = new List<IngredientAllergenModel>();
            List<IngredientsMasterAllergensEntity> allergenDetails = await _iingredientAllergenRepository.Get(ingSk);

            foreach (AllergenEntity allergen in allergens)
            {
                List<IngredientSubAllergenModel> allergenSuballergens = suballergens.Where(a => a.AllergenId == allergen.AllergenId)
                                                                                    .Select(sub => new IngredientSubAllergenModel
                                                                                    {
                                                                                        AllergenId = sub.AllergenId,
                                                                                        IngMasAllergenSk = allergenDetails.Where(x => x.SubAllergenId == sub.SubAllergenId && x.AllergenId == sub.AllergenId).Select(x => x.IngMasAllergenSk).SingleOrDefault(),
                                                                                        AllergenOptionId = allergenDetails.Where(x => x.SubAllergenId == sub.SubAllergenId && x.AllergenId == sub.AllergenId).Select(x => x.AllergenOptionId).SingleOrDefault(),
                                                                                        IngSk = allergenDetails.Where(x => x.SubAllergenId == sub.SubAllergenId && x.AllergenId == sub.AllergenId).Select(x => x.IngSk).SingleOrDefault(),
                                                                                        SubAllergenName = sub.Name,
                                                                                        SubAllergenId = sub.SubAllergenId
                                                                                    }).ToList();

                IngredientAllergenModel ingredientAllergenModel = new IngredientAllergenModel()
                {
                    AllergenId = allergen.AllergenId,
                    IngMasAllergenSk = allergenDetails.Where(x => x.AllergenId == allergen.AllergenId && x.SubAllergenId == null).Select(x => x.IngMasAllergenSk).SingleOrDefault(),
                    AllergenOptionId = allergenDetails.Where(x => x.AllergenId == allergen.AllergenId && x.SubAllergenId == null).Select(x => x.AllergenOptionId).SingleOrDefault(),
                    IngSk = allergenDetails.Where(x => x.AllergenId == allergen.AllergenId && x.SubAllergenId == null).Select(x => x.IngSk).SingleOrDefault(),
                    AllergenName = allergen.Name,
                    SubAllergen = allergenSuballergens
                };
                ingredientMasterAllergens.Add(ingredientAllergenModel);
            }

            return ingredientMasterAllergens;
        }

        public List<object> GetAllergenDetailsModel(List<IngredientAllergenModel> ings)
        {
            List<AllergenEntity> allergens = _iAllergenRepository.GetAll().GetAwaiter().GetResult();
            if (allergens == null || allergens.Count < 1)
                return null;

            ings = ings.Distinct().ToList();


            List<IngredientAllergenModel> distinctIngredients = ings.DistinctBy(x => x.IngSk).ToList();
            List<object> ingAllergens = new List<object>();

            foreach (IngredientAllergenModel ing in distinctIngredients)
            {
                //List<object> ingredientAllergenDetails = new List<object>();

                foreach (AllergenEntity allergen in allergens)
                {
                    IngredientAllergenModel? allergenDets = null;
                    if (ings != null && ings.Count > 0)
                    {
                        allergenDets = ings.Where(c => c.AllergenId == allergen.AllergenId && ing.IngSk == c.IngSk).FirstOrDefault();
                    }

                    IngredientAllergenModel ingAllergen = new IngredientAllergenModel()
                    {
                        AllergenName = allergen.Name,
                        AllergenId = allergenDets != null ? allergenDets.AllergenId : 0,
                        AllergenOptionId = allergenDets != null ? allergenDets.AllergenOptionId : null,
                        IngSk = ing.IngSk,
                        IngredientName = ing.IngredientName,
                    };
                    ingAllergens.Add(ingAllergen);
                }

                //var allergenDetails = new
                //{
                //    IngSk = ing.IngSk,
                //    IngredientName = ing.IngredientName,
                //    AllergenInfo = ingredientAllergenDetails
                //};
                //ingAllergens.Add(allergenDetails);
            }

            return ingAllergens;
        }
    }
}
