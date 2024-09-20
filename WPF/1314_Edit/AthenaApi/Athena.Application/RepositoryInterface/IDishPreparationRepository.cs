using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishPreparationRepository
    {
        public Task<bool> SaveDishPreparation(int dishSk, List<DishPreparationsReqModel> dishPreparations);
        public Task<bool> UpdateDishPreparation(int dishSk, List<DishPreparationsReqModel> dishPreparations, string userId);
        public Task<List<DishPreparationEntity>> GetDishPreparations(int dishSk);
    }
}
