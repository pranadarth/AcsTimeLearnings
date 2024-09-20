using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class IngredientCalorieRepository : IIngredientCalorieRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<IngredientCalorieRepository> _logger;

        public IngredientCalorieRepository(ILogger<IngredientCalorieRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<IngredientsMasterCaloric>> Get(long ingSk)
        {
            return await _athenaDbcontext.IngredientsMasterCaloric.Where(c => c.IngSk == ingSk).ToListAsync();
        }


        public async Task<bool> Save(List<SaveIngredientCaloricReqModel> caloricInfo)
        {
            long ingSk = caloricInfo.Select(i => i.IngSk).FirstOrDefault();

            List<IngredientsMasterCaloric> ingredientsMasterCalorics = await _athenaDbcontext.IngredientsMasterCaloric.Where(i => i.IngSk == ingSk).ToListAsync();
            foreach (SaveIngredientCaloricReqModel caloric in caloricInfo)
            {
                if (ingredientsMasterCalorics != null && ingredientsMasterCalorics.Count > 0)
                {
                    var ingredientsMasterCaloric = ingredientsMasterCalorics.Where(i => i.CaloricTypeSk == caloric.CaloricTypeSk).SingleOrDefault();
                    if (ingredientsMasterCaloric != null)
                    {
                        ingredientsMasterCaloric.Value = caloric.Value;
                        ingredientsMasterCaloric.ModifiedBy = caloric.UserId;
                        ingredientsMasterCaloric.ModifiedDate = DateTime.UtcNow;

                        await _athenaDbcontext.SaveChangesAsync();
                    }
                    else
                    {
                        IngredientsMasterCaloric newIngredientCaloricInfo = new IngredientsMasterCaloric()
                        {
                            CaloricTypeSk = caloric.CaloricTypeSk,
                            IngSk = caloric.IngSk,
                            Value = caloric.Value,
                            CreatedBy = caloric.UserId,
                            CreatedDate = DateTime.UtcNow
                        };

                        await _athenaDbcontext.IngredientsMasterCaloric.AddAsync(newIngredientCaloricInfo);
                    }
                }
                else
                {
                    IngredientsMasterCaloric newIngredientCaloricInfo = new IngredientsMasterCaloric()
                    {
                        CaloricTypeSk = caloric.CaloricTypeSk,
                        IngSk = caloric.IngSk,
                        Value = caloric.Value,
                        CreatedBy = caloric.UserId,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _athenaDbcontext.IngredientsMasterCaloric.AddAsync(newIngredientCaloricInfo);
                }

            }

            await _athenaDbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(List<UpdateIngredientCaloricInfoReqModel> caloricInfo)
        {

            long ingSk = caloricInfo.Select(i => i.IngSk).FirstOrDefault();

            List<IngredientsMasterCaloric> ingredientsMasterCalorics = await _athenaDbcontext.IngredientsMasterCaloric.Where(i => i.IngSk == ingSk).ToListAsync();
            foreach (UpdateIngredientCaloricInfoReqModel caloric in caloricInfo)
            {
                if (ingredientsMasterCalorics != null && ingredientsMasterCalorics.Count > 0)
                {
                    var ingredientsMasterCaloric = ingredientsMasterCalorics.Where(i => i.CaloricTypeSk == caloric.CaloricTypeSk).SingleOrDefault();
                    if (ingredientsMasterCaloric != null)
                    {
                        ingredientsMasterCaloric.Value = caloric.Value;
                        ingredientsMasterCaloric.ModifiedBy = caloric.UserId;
                        ingredientsMasterCaloric.ModifiedDate = DateTime.UtcNow;

                        await _athenaDbcontext.SaveChangesAsync();
                    }
                    else
                    {
                        IngredientsMasterCaloric newIngredientCaloricInfo = new IngredientsMasterCaloric()
                        {
                            CaloricTypeSk = caloric.CaloricTypeSk,
                            IngSk = caloric.IngSk,
                            Value = caloric.Value,
                            CreatedBy = caloric.UserId,
                            CreatedDate = DateTime.UtcNow
                        };

                        await _athenaDbcontext.IngredientsMasterCaloric.AddAsync(newIngredientCaloricInfo);
                    }
                }
                else
                {
                    IngredientsMasterCaloric newIngredientCaloricInfo = new IngredientsMasterCaloric()
                    {
                        CaloricTypeSk = caloric.CaloricTypeSk,
                        IngSk = caloric.IngSk,
                        Value = caloric.Value,
                        CreatedBy = caloric.UserId,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _athenaDbcontext.IngredientsMasterCaloric.AddAsync(newIngredientCaloricInfo);
                }
            }

            await _athenaDbcontext.SaveChangesAsync();

            return true;
        }


        #region Reports
        public async Task<object> GetNutritionalHeaderDetails()
        {
            var result = await (from im in _athenaDbcontext.IngredientsMaster
                                join imc in _athenaDbcontext.IngredientsMasterCaloric on im.IngSk equals imc.IngSk into j
                                from imc in j.DefaultIfEmpty()
                                group new { im, imc } by im.SupplierId into g
                                select new
                                {
                                    SupplierId = g.Key,
                                    SuppierName = g.Where(x => x.im.SupplierId == g.Key).Select(x => x.im.SupplierName).Distinct().FirstOrDefault(),
                                    Missing = g.Where(x => x.im.SupplierId == g.Key && x.imc.IngSk == null).GroupBy(x => x.imc.IngSk).Count(),
                                    Provided = g.Where(x => x.im.SupplierId == g.Key && x.imc.IngSk != null).GroupBy(x => x.imc.IngSk).Count(),
                                    MCWUsed = g.Count(x => x.im.SupplierName == "MCW"),
                                    Total = g.GroupBy(x => x.imc.IngSk).Count()
                                }).ToListAsync();


            return result;
        }

        public async Task<List<IngredientCalorieModel>> GetNutritionalDetails(int supplierId, string status)
        {
            if (status.ToLower() == "missing")
            {
                var query = await (from im in _athenaDbcontext.IngredientsMaster
                                   join icom in _athenaDbcontext.IngredientsMasterCaloric
                                   on im.IngSk equals icom.IngSk into joined
                                   from icom in joined.DefaultIfEmpty()
                                   where im.SupplierId == supplierId && icom.IngSk == null
                                   select new IngredientCalorieModel
                                   {
                                       IngSk = im.IngSk,
                                       IngMastCaloricSK = icom.IngredientMasterCaloricSk,
                                       IngredientName = im.IngredientsName,
                                       CalTypeSk = icom.CaloricTypeSk,
                                       Value = icom.Value
                                   }).ToListAsync();
                return query;
            }
            else if (status.ToLower() == "provided")
            {
                var query = await (from im in _athenaDbcontext.IngredientsMaster
                                   join icom in _athenaDbcontext.IngredientsMasterCaloric
                                   on im.IngSk equals icom.IngSk into joined
                                   from icom in joined.DefaultIfEmpty()
                                   where im.SupplierId == supplierId && icom.IngSk != null
                                   select new IngredientCalorieModel
                                   {
                                       IngSk = im.IngSk,
                                       IngMastCaloricSK = icom.IngredientMasterCaloricSk,
                                       IngredientName = im.IngredientsName,
                                       CalTypeSk = icom.CaloricTypeSk,
                                       Value = icom.Value
                                   }).ToListAsync();
                return query;
            }
            else if (status.ToLower() == "mcwused")
            {
                var mcwused = await (from im in _athenaDbcontext.IngredientsMaster
                                     join icom in _athenaDbcontext.IngredientsMasterCaloric
                                     on im.IngSk equals icom.IngSk into joined
                                     from icom in joined.DefaultIfEmpty()
                                     where im.SupplierId == supplierId && icom.IngSk != null
                                     && im.SupplierName == "MCW"
                                     select new IngredientCalorieModel
                                     {
                                         IngSk = im.IngSk,
                                         IngMastCaloricSK = icom.IngredientMasterCaloricSk,
                                         IngredientName = im.IngredientsName,
                                         CalTypeSk = icom.CaloricTypeSk,
                                         Value = icom.Value
                                     }).ToListAsync();
                return mcwused;
            }

            return null;
        }
        #endregion
    }
}
