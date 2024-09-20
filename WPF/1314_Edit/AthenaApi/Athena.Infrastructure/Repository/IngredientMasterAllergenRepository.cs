using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class IngredientMasterAllergenRepository : IIngredientMasterAllergenRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<IngredientMasterAllergenRepository> _logger;

        public IngredientMasterAllergenRepository(ILogger<IngredientMasterAllergenRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<IngredientsMasterAllergensEntity>> Get(long ingSk)
        {
            return await _athenaDbcontext.IngredientsMasterAllergensEntity.Where(c => c.IngSk == ingSk).ToListAsync();
        }

        public async Task<bool> Save(List<SaveIngredientAllergenRequestModel> allergens)
        {
            long ingSk = allergens.Select(i => i.IngSk).FirstOrDefault();

            List<IngredientsMasterAllergensEntity> ingredientsMasterAllergens = await _athenaDbcontext.IngredientsMasterAllergensEntity.Where(i => i.IngSk == ingSk).ToListAsync();
            foreach (SaveIngredientAllergenRequestModel allergen in allergens)
            {
                if (ingredientsMasterAllergens != null && ingredientsMasterAllergens.Count > 0)
                {
                    var masterAllergen = ingredientsMasterAllergens.Where(i => i.AllergenId == allergen.AllergenId && i.SubAllergenId == allergen.SubAllergenId).SingleOrDefault();
                    if (masterAllergen != null)
                    {
                        masterAllergen.AllergenOptionId = allergen.AllergenOptionId;
                        masterAllergen.ModifiedBy = allergen.UserId;
                        masterAllergen.ModifiedDate = DateTime.UtcNow;

                        await _athenaDbcontext.SaveChangesAsync();
                    }
                    else
                    {
                        IngredientsMasterAllergensEntity newIngredientAllergen = new IngredientsMasterAllergensEntity()
                        {
                            IngSk = allergen.IngSk,
                            AllergenId = allergen.AllergenId,
                            SubAllergenId = allergen.SubAllergenId,
                            AllergenOptionId = allergen.AllergenOptionId,
                            CreatedBy = allergen.UserId,
                            CreatedDate = DateTime.UtcNow
                        };

                        await _athenaDbcontext.IngredientsMasterAllergensEntity.AddAsync(newIngredientAllergen);
                    }
                }
                else
                {
                    IngredientsMasterAllergensEntity newIngredientAllergen = new IngredientsMasterAllergensEntity()
                    {
                        IngSk = allergen.IngSk,
                        AllergenId = allergen.AllergenId,
                        SubAllergenId = allergen.SubAllergenId,
                        AllergenOptionId = allergen.AllergenOptionId,
                        CreatedBy = allergen.UserId,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _athenaDbcontext.IngredientsMasterAllergensEntity.AddAsync(newIngredientAllergen);
                }

            }

            await _athenaDbcontext.SaveChangesAsync();
            return true;
        }

        #region Reports
        public async Task<object> GetAllergenHeaderDetails()
        {
            var result = await (from im in _athenaDbcontext.IngredientsMaster
                                join imc in _athenaDbcontext.IngredientsMasterAllergensEntity on im.IngSk equals imc.IngSk into j
                                from imc in j.DefaultIfEmpty()
                                group new { im, imc } by im.SupplierId into g
                                select new
                                {
                                    SupplierId = g.Key,
                                    SuppierName = g.Where(x => x.im.SupplierId == g.Key).Select(x => x.im.SupplierName).Distinct().FirstOrDefault(),
                                    Missing = g.Where(x => x.im.SupplierId == g.Key && x.imc.IngSk == null).GroupBy(x => x.imc.IngSk).Count(),
                                    Provided = g.Where(x => x.im.SupplierId == g.Key && x.imc.IngSk != null).GroupBy(x => x.imc.IngSk).Count(),
                                    Total = g.GroupBy(x => x.imc.IngSk).Count()
                                }).ToListAsync();


            return result;
        }

        public async Task<List<IngredientAllergenModel>> GetAllergenDetails(int supplierId, string status)
        {
            if (status.ToLower() == "missing")
            {
                var query = await (from im in _athenaDbcontext.IngredientsMaster
                                   join icom in _athenaDbcontext.IngredientsMasterAllergensEntity
                                   on im.IngSk equals icom.IngSk into joined
                                   from icom in joined.DefaultIfEmpty()
                                   where im.SupplierId == supplierId && icom.IngSk == null
                                   select new IngredientAllergenModel
                                   {
                                       IngSk = im.IngSk,
                                       IngMasAllergenSk = icom.IngMasAllergenSk,
                                       IngredientName = im.IngredientsName,
                                       AllergenId = icom.AllergenId,
                                       AllergenOptionId = icom.AllergenOptionId
                                   }).ToListAsync();
                return query;
            }
            else if (status.ToLower() == "provided")
            {
                var query = await (from im in _athenaDbcontext.IngredientsMaster
                                   join icom in _athenaDbcontext.IngredientsMasterAllergensEntity
                                   on im.IngSk equals icom.IngSk into joined
                                   from icom in joined.DefaultIfEmpty()
                                   where im.SupplierId == supplierId && icom.IngSk != null
                                   select new IngredientAllergenModel
                                   {
                                       IngSk = im.IngSk,
                                       IngMasAllergenSk = icom.IngMasAllergenSk,
                                       IngredientName = im.IngredientsName,
                                       AllergenId = icom.AllergenId,
                                       AllergenOptionId = icom.AllergenOptionId
                                   }).ToListAsync();
                return query;
            }

            return null;
        }
        #endregion
    }
}
