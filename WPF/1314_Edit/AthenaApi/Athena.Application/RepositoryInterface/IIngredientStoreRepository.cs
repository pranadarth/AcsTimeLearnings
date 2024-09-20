﻿using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IIngredientStoreRepository
    {
        public Task<IngredientStore> GetById(int id);
        public Task<List<IngredientStore>> GetStores();
        public Task<IngredientStore> GetByCode(string ingStoreCode);
    }
}
