using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class IngredientStoreService: IIngredientStoreService
    {
        private readonly IIngredientStoreRepository _ingredientStoreRepository;

        public IngredientStoreService(IIngredientStoreRepository ingredientStoreRepository)
        {
            _ingredientStoreRepository = ingredientStoreRepository;
        }

        public async Task<List<IngredientStore>> GetStores()
        {
            return await _ingredientStoreRepository.GetStores();
        }

        public async Task<IngredientStore> GetByCode(string ingStoreCode)
        {
            return await _ingredientStoreRepository.GetByCode(ingStoreCode);
        }

        public async Task<IngredientStore> GetById(int id)
        {
            return await _ingredientStoreRepository.GetById(id);
        }
    }
}
