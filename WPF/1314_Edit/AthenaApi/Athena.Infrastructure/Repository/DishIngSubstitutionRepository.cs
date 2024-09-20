using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class DishIngSubstitutionRepository : IDishIngSubstitutionRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishIngSubstitutionRepository> _logger;

        public DishIngSubstitutionRepository(ILogger<DishIngSubstitutionRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<bool> SaveIngredientSubstitution(long preIngSk, long postIngSk, List<SubstituteOnDish> substituteOnDishes, string userId)
        {
            foreach (SubstituteOnDish substituteOnDish in substituteOnDishes)
            {
                DishSubstitutionDetailsEntity dishSubstitutionEntity = new DishSubstitutionDetailsEntity()
                {
                    DishSk = substituteOnDish.DishSk,
                    PostIngSk = postIngSk,
                    PreIngSk = preIngSk,
                    PreSaleCost = substituteOnDish.PreSaleCost,
                    PostSaleCost = substituteOnDish.PostSaleCost,
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow
                };
                await _athenaDbcontext.DishSubstitutionDetailsEntity.AddAsync(dishSubstitutionEntity);
            }

            await _athenaDbcontext.SaveChangesAsync();

            return true;
        }
    }
}
