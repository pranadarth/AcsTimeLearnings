using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
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
    public class IngredientStoreRepository : IIngredientStoreRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<IngredientStoreRepository> _logger;

        public IngredientStoreRepository(ILogger<IngredientStoreRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<IngredientStore> GetById(int id)
        {
            return await _athenaDbcontext.IngredientStore.Where(x => x.StoreSk == id && x.ActiveStatus == true).SingleOrDefaultAsync();
        }

        public async Task<List<IngredientStore>> GetStores()
        {
            return await _athenaDbcontext.IngredientStore.Where(i => i.ActiveStatus == true).ToListAsync();
        }

        public async Task<IngredientStore> GetByCode(string ingStoreCode)
        {
            return await _athenaDbcontext.IngredientStore.Where(x => x.StoreCode == ingStoreCode && x.ActiveStatus == true).SingleOrDefaultAsync();
        }
    }
}
