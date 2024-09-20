using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishIngredientsRepository
    {
        public Task<bool> SaveDishIngredients(int dishSk, List<DishIngredientReqModel> dishIngredientDetails);
        public Task<bool> UpdateDishIngredients(int dishSk, List<DishIngredientReqModel> dishIngredientDetails, string userId);

        public Task<List<DishIngredientEntity>> GetDishIngredients(int dishSk);

        public Task<bool> SubstituteDishIngredients(long preIngSk, long postIngSk, List<SubstituteOnDish> substituteOnDishes, string userId);

    }
}
