using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishMenuMappingRepository
    {
        public Task<bool> SaveDishMenuMapping(int dishSk, List<int> dishMenuTypeIds);
        public Task<bool> UpdateDishMenuMapping(int dishSk, List<int> dishMenuTypeIds);
        public Task<List<DishMenuMappingEntity>> GetDishMenuTypes(List<int> dishSks);
        public Task<List<DishMenuMappingEntity>> GetDishMenuTypes(int dishSk);
    }
}
