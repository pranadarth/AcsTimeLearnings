using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishMealMappingRepository
    {
        public Task<bool> SaveDishMealMapping(int dishSk, List<int> dishMealTypeIds);
        public Task<bool> UpdateDishMealMapping(int dishSk, List<int> dishMealTypeIds);
        public Task<List<DishMealMappingEntity>> GetDishMealTypes(List<int> dishSks);
        public Task<List<DishMealMappingEntity>> GetDishMealTypes(int dishSk);
    }
}
