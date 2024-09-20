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
    public class DishMealMappingRepository : IDishMealMappingRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishMealMappingRepository> _logger;

        public DishMealMappingRepository(ILogger<DishMealMappingRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<bool> SaveDishMealMapping(int dishSk, List<int> dishMealTypeIds)
        {
            foreach (int dishMealTypeId in dishMealTypeIds)
            {
                DishMealMappingEntity newDishMealMapping = new DishMealMappingEntity()
                {
                    DishSk = dishSk,
                    MealTypeId = dishMealTypeId,
                    ActiveStatus = true
                };

                await _athenaDbcontext.DishMealMappingEntity.AddAsync(newDishMealMapping);
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateDishMealMapping(int dishSk, List<int> dishMealTypeIds)
        {
            List<DishMealMappingEntity> dishMealMappings = await _athenaDbcontext.DishMealMappingEntity.Where(d => d.DishSk == dishSk).ToListAsync();
            if (dishMealMappings != null && dishMealMappings.Count > 0)
            {
                List<DishMealMappingEntity> dishMealMappingsToDelete = dishMealMappings.Where(d => d.MealTypeId != null && !dishMealTypeIds.Contains(d.MealTypeId.Value)).ToList();
                if (dishMealMappingsToDelete != null && dishMealMappingsToDelete.Count > 0)
                {
                    _athenaDbcontext.DishMealMappingEntity.RemoveRange(dishMealMappingsToDelete);

                    List<int?> existingMealTypesIds = dishMealMappings.Select(d => d.MealTypeId).ToList();

                    dishMealTypeIds = dishMealTypeIds.Where(l => !existingMealTypesIds.Contains(l)).ToList();
                }
            }

            await SaveDishMealMapping(dishSk, dishMealTypeIds);
            return true;
        }

        public async Task<List<DishMealMappingEntity>> GetDishMealTypes(List<int> dishSks)
        {
            return await _athenaDbcontext.DishMealMappingEntity.Include(d => d.DishMealType).Where(d => d.DishSk != null & dishSks.Contains(d.DishSk.Value)).ToListAsync();
        }

        public async Task<List<DishMealMappingEntity>> GetDishMealTypes(int dishSk)
        {
            return await _athenaDbcontext.DishMealMappingEntity.Include(d => d.DishMealType).Where(d => d.DishSk == dishSk).ToListAsync();
        }
    }
}
