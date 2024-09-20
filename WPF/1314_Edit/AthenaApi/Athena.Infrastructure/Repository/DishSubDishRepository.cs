using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class DishSubDishRepository : IDishSubDishRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishSubDishRepository> _logger;

        public DishSubDishRepository(ILogger<DishSubDishRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishSubDishEntity>> GetSubDishes(int dishSk)
        {
            return await _athenaDbcontext.DishSubDishEntity.Where(d => d.DishSk == dishSk).ToListAsync();
        }

        public async Task<bool> SaveSubDish(int dishSk, List<SubDishesReqModel> subDishes)
        {
            foreach (SubDishesReqModel subDish in subDishes)
            {
                DishSubDishEntity newDishSubDishEntity = new DishSubDishEntity()
                {
                    DishSk = dishSk,
                    Quantity = subDish.Quantity,
                    Units = subDish.Units,
                    Cost = subDish.Cost
                };

                await _athenaDbcontext.DishSubDishEntity.AddAsync(newDishSubDishEntity);
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateSubDish(int dishSk, List<SubDishesReqModel> reqSubDishes, string userId)
        {
            List<DishSubDishEntity> existingDishSubDishes = await _athenaDbcontext.DishSubDishEntity.Where(i => i.DishSk == dishSk).ToListAsync();
            if (existingDishSubDishes.Any())
            {
                List<int> subDishIds = reqSubDishes.Select(i => i.SubDishSk).ToList();

                List<DishSubDishEntity> dishSubDishsToDelete = existingDishSubDishes.Where(i => !subDishIds.Contains(i.SubDishSk)).ToList();

                if (dishSubDishsToDelete.Any())
                    _athenaDbcontext.DishSubDishEntity.RemoveRange(dishSubDishsToDelete);

                List<DishSubDishEntity> dishSubDishsToUpdate = existingDishSubDishes.Where(i => subDishIds.Contains(i.SubDishSk)).ToList();
                if (dishSubDishsToUpdate.Any())
                {
                    foreach (DishSubDishEntity dishSubDish in existingDishSubDishes)
                    {
                        SubDishesReqModel? reqDishSubDishToUpdate = reqSubDishes.Where(i => i.SubDishSk == dishSubDish.SubDishSk).SingleOrDefault();
                        if (reqDishSubDishToUpdate != null)
                        {
                            dishSubDish.Quantity = reqDishSubDishToUpdate.Quantity;
                            dishSubDish.Units = reqDishSubDishToUpdate.Units;
                            dishSubDish.Cost = reqDishSubDishToUpdate.Cost;
                            //dishSubDish.ModifiedBy = userId;
                            //dishSubDish.ModifiedDate = DateTime.UtcNow;
                        }
                    }
                }
                await _athenaDbcontext.SaveChangesAsync();

                List<int> existingDishSubDishIds = existingDishSubDishes.Select(i => i.SubDishSk).ToList();
                if (existingDishSubDishIds != null && existingDishSubDishIds.Count > 0)
                    reqSubDishes = reqSubDishes.Where(i => !existingDishSubDishIds.Contains(i.SubDishSk)).ToList();
            }

            if (reqSubDishes.Any())
                await SaveSubDish(dishSk, reqSubDishes);

            return true;
        }
    }
}
