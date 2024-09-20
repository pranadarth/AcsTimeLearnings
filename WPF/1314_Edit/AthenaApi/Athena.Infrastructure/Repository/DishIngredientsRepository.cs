using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class DishIngredientsRepository : IDishIngredientsRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishIngredientsRepository> _logger;

        public DishIngredientsRepository(ILogger<DishIngredientsRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishIngredientEntity>> GetDishIngredients(int dishSk)
        {
            return await _athenaDbcontext.DishIngredientEntity.Where(i => i.DishSk == dishSk).ToListAsync();
        }

        public async Task<bool> SaveDishIngredients(int dishSk, List<DishIngredientReqModel> dishIngredientDetails)
        {
            foreach (DishIngredientReqModel dishIngredientDetail in dishIngredientDetails)
            {
                DishIngredientEntity newDishIngredient = new DishIngredientEntity()
                {
                    DishSk = dishSk,
                    IngSk = dishIngredientDetail.IngSk,
                    SupplierId = dishIngredientDetail.SupplierId,
                    Quantity = dishIngredientDetail.Quantity,
                    MeasureOptionId = dishIngredientDetail.MeasureOptionId,
                    Weight = dishIngredientDetail.Weight,
                    UnitCost = dishIngredientDetail.UnitCost,
                    CostExtension = dishIngredientDetail.CostExtension,
                    //CreatedBy = reqData.UserId,
                    //CreatedDate = DateTime.UtcNow
                };

                await _athenaDbcontext.DishIngredientEntity.AddAsync(newDishIngredient);
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateDishIngredients(int dishSk, List<DishIngredientReqModel> reqDishIngs, string userId)
        {
            List<DishIngredientEntity> existingDishIngredients = await _athenaDbcontext.DishIngredientEntity.Where(i => i.DishSk == dishSk).ToListAsync();
            if (existingDishIngredients.Any())
            {
                List<long> requestIngs = reqDishIngs.Select(i => i.IngSk).ToList();

                List<DishIngredientEntity> dishIngsToDelete = existingDishIngredients.Where(i => !requestIngs.Contains(i.IngSk)).ToList();

                if (dishIngsToDelete.Any())
                    _athenaDbcontext.DishIngredientEntity.RemoveRange(dishIngsToDelete);

                List<DishIngredientEntity> dishIngsToUpdate = existingDishIngredients.Where(i => requestIngs.Contains(i.IngSk)).ToList();
                if (dishIngsToUpdate.Any())
                {
                    foreach (DishIngredientEntity dishIng in existingDishIngredients)
                    {
                        DishIngredientReqModel? reqDishIngToUpdate = reqDishIngs.Where(i => i.IngSk == dishIng.IngSk).SingleOrDefault();
                        if (reqDishIngToUpdate != null)
                        {
                            dishIng.SupplierId = reqDishIngToUpdate.SupplierId;
                            dishIng.Quantity = reqDishIngToUpdate.Quantity;
                            dishIng.MeasureOptionId = reqDishIngToUpdate.MeasureOptionId;
                            dishIng.Weight = reqDishIngToUpdate.Weight;
                            dishIng.UnitCost = reqDishIngToUpdate.UnitCost;
                            dishIng.CostExtension = reqDishIngToUpdate.CostExtension;
                            dishIng.ModifiedBy = userId;
                            dishIng.ModifiedDate = DateTime.UtcNow;
                        }
                    }
                }
                await _athenaDbcontext.SaveChangesAsync();

                List<long> existingDishIngIds = existingDishIngredients.Select(i => i.IngSk).ToList();
                if (existingDishIngIds != null && existingDishIngIds.Count > 0)
                    reqDishIngs = reqDishIngs.Where(i => !existingDishIngIds.Contains(i.IngSk)).ToList();
            }

            if (reqDishIngs.Any())
                await SaveDishIngredients(dishSk, reqDishIngs);

            return true;
        }

        public async Task<bool> SubstituteDishIngredients(long preIngSk, long postIngSk, List<SubstituteOnDish> substituteOnDishes, string userId)
        {
            List<int> dishSks = substituteOnDishes.Select(d => d.DishSk).ToList();

            List<DishIngredientEntity> existingDishIngredients = await _athenaDbcontext.DishIngredientEntity.Where(i => dishSks.Contains(i.DishSk) && i.IngSk == preIngSk).ToListAsync();
            if (existingDishIngredients.Any())
            {
                foreach (DishIngredientEntity dishIng in existingDishIngredients)
                {
                    dishIng.IngSk = postIngSk;
                    dishIng.ModifiedBy = userId;
                    dishIng.ModifiedDate = DateTime.UtcNow;
                }
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }
    }
}
