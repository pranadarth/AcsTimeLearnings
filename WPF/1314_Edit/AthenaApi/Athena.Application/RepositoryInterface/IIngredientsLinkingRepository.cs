using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IIngredientsLinkingRepository
    {
        public Task<bool> SaveIngredientLinking(SaveIngredientLinkingReqModel reqData);
        public Task<List<long>> GetDestinationIngredientLinkingStatus(List<long> ingSks);
        public Task<List<IngredientsLinkingEntity>> GetIngredientLinking(long ingSk);
    }
}
