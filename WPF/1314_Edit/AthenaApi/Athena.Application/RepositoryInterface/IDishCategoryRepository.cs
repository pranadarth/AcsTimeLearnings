﻿using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishCategoryRepository
    {
        public Task<List<DishCategoryEntity>> GetDishCategories();
    }
}
