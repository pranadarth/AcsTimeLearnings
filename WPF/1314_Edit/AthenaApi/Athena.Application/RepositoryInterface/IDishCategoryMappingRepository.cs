using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishCategoryMappingRepository
    {
        public Task<bool> SaveDishCategoryMapping(int dishSk, List<int> categoryIds);

        public Task<bool> UpdateDishCategoryMapping(int dishSk, List<int> categoryIds);

        public Task<List<DishCategoryMappingEntity>> GetDishCategoryMapping(int dishSk);
        public Task<List<DishCategoryMappingEntity>> GetDishCategoryMapping(List<int> dishSks);
    }
}
