using Athena.Domain.Entities;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public class DishMenuMappingRepository : IDishMenuMappingRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishMenuMappingRepository> _logger;

        public DishMenuMappingRepository(ILogger<DishMenuMappingRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<bool> SaveDishMenuMapping(int dishSk, List<int> dishMenuTypeIds)
        {
            foreach (int? dishMenuTypeId in dishMenuTypeIds)
            {
                if (dishMenuTypeId == null)
                    continue;

                DishMenuMappingEntity newDishMenuMapping = new DishMenuMappingEntity()
                {
                    DishSk = dishSk,
                    DishMenuTypeId = dishMenuTypeId,
                    ActiveStatus = true
                };

                await _athenaDbcontext.DishMenuMappingEntity.AddAsync(newDishMenuMapping);
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateDishMenuMapping(int dishSk, List<int> dishMenuTypeIds)
        {
            List<DishMenuMappingEntity> dishMenuMappings = _athenaDbcontext.DishMenuMappingEntity.Where(d => d.DishSk == dishSk).ToList();
            if (dishMenuMappings != null && dishMenuMappings.Count > 0)
            {
                List<DishMenuMappingEntity> dishMenuMappingsToDelete = dishMenuMappings.Where(d => d.DishMenuTypeId != null && !dishMenuTypeIds.Contains(d.DishMenuTypeId.Value)).ToList();
                if (dishMenuMappingsToDelete != null && dishMenuMappingsToDelete.Count > 0)
                {
                    _athenaDbcontext.DishMenuMappingEntity.RemoveRange(dishMenuMappingsToDelete);

                    List<int?> existingMenuTypesIds = dishMenuMappings.Select(d => d.DishMenuTypeId).ToList();

                    dishMenuTypeIds = dishMenuTypeIds.Where(l => !existingMenuTypesIds.Contains(l)).ToList();
                }
            }

            await SaveDishMenuMapping(dishSk, dishMenuTypeIds);
            return true;
        }

        public async Task<List<DishMenuMappingEntity>> GetDishMenuTypes(List<int> dishSks)
        {
            return await _athenaDbcontext.DishMenuMappingEntity.Include(d => d.DishMenuType).Where(d => d.DishSk != null && dishSks.Contains(d.DishSk.Value)).ToListAsync();
        }

        public async Task<List<DishMenuMappingEntity>> GetDishMenuTypes(int dishSk)
        {
            return await _athenaDbcontext.DishMenuMappingEntity.Include(d => d.DishMenuType).Where(d => d.DishSk == dishSk).ToListAsync();
        }
    }
}
