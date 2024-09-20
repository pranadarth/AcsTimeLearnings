using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishSubDishRepository
    {
        public Task<bool> SaveSubDish(int dishSk, List<SubDishesReqModel> subDishesModel);
        public Task<bool> UpdateSubDish(int dishSk, List<SubDishesReqModel> subDishesModel, string userId);

        public Task<List<DishSubDishEntity>> GetSubDishes(int dishSk);
    }
}
