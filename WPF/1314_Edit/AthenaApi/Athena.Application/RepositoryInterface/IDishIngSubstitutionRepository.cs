using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishIngSubstitutionRepository
    {
        public Task<bool> SaveIngredientSubstitution(long preIngSk, long postIngSk, List<SubstituteOnDish> substituteOnDishes, string userId);
    }
}
