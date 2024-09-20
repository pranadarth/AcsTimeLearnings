using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface ISubDishRepository
    {
        public Task<bool> SaveSubDish(int dishSk, SubDishesReqModel subDishes);
    }
}
