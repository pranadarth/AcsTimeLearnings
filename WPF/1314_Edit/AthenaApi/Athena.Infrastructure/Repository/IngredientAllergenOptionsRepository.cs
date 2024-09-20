using Athena.Application.RepositoryInterface;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Athena.Infrastructure.Repository
{
    public class IngredientAllergenOptionsRepository : IIngredientAllergenOptionsRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<IngredientAllergenOptionsRepository> _logger;

        public IngredientAllergenOptionsRepository(ILogger<IngredientAllergenOptionsRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }


        public async Task<object> GetAllergenOptions()
        {
            var query = await (from aom in _athenaDbcontext.AllergenOptionMappingEntity
                               join ao in _athenaDbcontext.AllergenOptionEntity on aom.AllergenOptionId equals ao.AllergenOptionId
                               join a in _athenaDbcontext.AllergenEntity on aom.AllergenId equals a.AllergenId
                               join sa in _athenaDbcontext.SubAllergensEntity on a.AllergenId equals sa.AllergenId into saGroup
                               from subAllergen in saGroup.DefaultIfEmpty()
                               where ao.ActiveStatus == true && aom.ActiveStatus == true && a.ActiveStatus == true || subAllergen.ActiveStatus == true
                               select new
                               {
                                   AllergenId = a.AllergenId,
                                   Allergen = a.Name,
                                   SubAllergen = subAllergen != null ? subAllergen.Name : null,
                                   AllergenOption = ao.Allergen_Option,
                                   AllergenOptionId = ao.AllergenOptionId,
                                   DisplayOrder = aom.DisplayOrder
                               }).ToListAsync();

            return query;
        }
    }
}
