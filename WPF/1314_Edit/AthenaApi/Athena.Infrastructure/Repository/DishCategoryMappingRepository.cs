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
    public class DishCategoryMappingRepository : IDishCategoryMappingRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishCategoryMappingRepository> _logger;

        public DishCategoryMappingRepository(ILogger<DishCategoryMappingRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishCategoryMappingEntity>> GetDishCategoryMapping(int dishSk)
        {
           return await _athenaDbcontext.DishCategoryMappingEntity.Where(d => d.DishSk == dishSk).ToListAsync();
        }

        public async Task<List<DishCategoryMappingEntity>> GetDishCategoryMapping(List<int> dishSks)
        {
            return await _athenaDbcontext.DishCategoryMappingEntity.Where(d => dishSks.Contains(d.DishSk)).ToListAsync();
        }

        public async Task<bool> SaveDishCategoryMapping(int dishSk, List<int> categoryIds)
        {
            foreach (int categoryId in categoryIds)
            {
                DishCategoryMappingEntity newDishCategory = new DishCategoryMappingEntity()
                {
                    DishSk = dishSk,
                    DishCategoryId = categoryId,
                };

                await _athenaDbcontext.DishCategoryMappingEntity.AddAsync(newDishCategory);
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateDishCategoryMapping(int dishSk, List<int> categoryIds)
        {

            List<DishCategoryMappingEntity> dishCategories = _athenaDbcontext.DishCategoryMappingEntity.Where(d => d.DishSk == dishSk).ToList();
            if (dishCategories != null && dishCategories.Count > 0)
            {
                List<DishCategoryMappingEntity> dishCategoriesToDelete = dishCategories.Where(d => !categoryIds.Contains(d.DishCategoryId)).ToList();
                if (dishCategoriesToDelete != null && dishCategoriesToDelete.Count > 0)
                {
                    _athenaDbcontext.DishCategoryMappingEntity.RemoveRange(dishCategoriesToDelete);

                    List<int> existingCategoryIds = dishCategories.Select(d => d.DishCategoryId).ToList();

                    categoryIds = categoryIds.Where(l => !existingCategoryIds.Contains(l)).ToList();
                }
            }

            await SaveDishCategoryMapping(dishSk, categoryIds);
            return true;
        }
    }
}
