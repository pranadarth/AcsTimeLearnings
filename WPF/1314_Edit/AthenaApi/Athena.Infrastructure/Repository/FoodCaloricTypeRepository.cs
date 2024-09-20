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
    public class FoodCaloricTypeRepository : IFoodCaloricTypeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<FoodCaloricTypeRepository> _logger;

        public FoodCaloricTypeRepository(ILogger<FoodCaloricTypeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }


        public async Task<List<FoodCaloricTypeEntity>> GetAll()
        {
            return await _athenaDbcontext.FoodCaloricTypeEntity.Where(c => c.ActiveStatus == true).ToListAsync();
        }
    }
}
