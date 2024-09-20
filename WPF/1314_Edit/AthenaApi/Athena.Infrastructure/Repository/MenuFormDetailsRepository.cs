using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Athena.Infrastructure.Repository
{
    public class MenuFormDetailsRepository : IMenuFormDetailsRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<MenuFormDetailsRepository> _logger;

        public MenuFormDetailsRepository(ILogger<MenuFormDetailsRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<int> AddMenuFormDetails(AddMenuFormDetailsReqModel addMenuFormDetailsReqModel)
        {
            MenuFormDetailsEntity newDish = new MenuFormDetailsEntity()
            {
                MenuFormDate = addMenuFormDetailsReqModel.MenuFormDate,
                MenuFormWeekDate = addMenuFormDetailsReqModel.MenuFormWeekDate,
                MenuFormMealCourseSk = addMenuFormDetailsReqModel.MenuFormMealCourseSk,
                LocationMenuMapId = addMenuFormDetailsReqModel.LocationMenuMapId,
                DishSk = addMenuFormDetailsReqModel.DishSk,
                MealTypeId = addMenuFormDetailsReqModel.MealTypeId,
                DishMenuTypeId = addMenuFormDetailsReqModel.DishMenuTypeId,
                Quantity = addMenuFormDetailsReqModel.Quantity,
                ActiveStatus = addMenuFormDetailsReqModel.ActiveStatus,
                CreatedBy = addMenuFormDetailsReqModel.UserId,
                CreatedDate = DateTime.UtcNow
            };

            await _athenaDbcontext.MenuFormDetailsEntity.AddAsync(newDish);
            await _athenaDbcontext.SaveChangesAsync();

            return newDish.MenuFormDetailsSk;
        }

        public async Task<bool> UpdateMenuFormDetails(int menuFormMealCourseSk, List<int> dishSks, string userId, DateTime menuFormDate, DateTime menuFormWeekDate, int locationMenuMapId, int mealTypeId, int dishMenuTypeId, int quantity, bool activeStatus)
        {
            List<MenuFormDetailsEntity> existingDishes = await _athenaDbcontext.MenuFormDetailsEntity.Where(i => menuFormMealCourseSk == i.MenuFormMealCourseSk).ToListAsync();
            if (existingDishes.Any())
            {
                List<MenuFormDetailsEntity> dishsToDelete = existingDishes.Where(i => !dishSks.Contains(i.DishSk.Value)).ToList();

                if (dishsToDelete.Any())
                    _athenaDbcontext.MenuFormDetailsEntity.RemoveRange(dishsToDelete);

                List<MenuFormDetailsEntity> dishSubDishsToUpdate = existingDishes.Where(i => dishSks.Contains(i.DishSk.Value)).ToList();
                if (dishSubDishsToUpdate.Any())
                {
                    //    //foreach (MenuFormDetailsEntity dish in dishSubDishsToUpdate)
                    //    //{
                    //    //    int? dishSkToUpdate = dishSks.Where(i => i == dish.DishSk).SingleOrDefault();

                    //    //    if (dishSkToUpdate != null)
                    //    //    {
                    //    //        dish.DishSk = dishSkToUpdate;
                    //    //        dish.ModifiedBy = userId;
                    //    //        dish.ModifiedDate = DateTime.UtcNow;
                    //    //    }
                    //    //}

                    List<int> existingDishSubDishIds = dishSubDishsToUpdate.Select(i => i.DishSk.Value).ToList();
                    if (existingDishSubDishIds != null && existingDishSubDishIds.Count > 0)
                        dishSks = dishSks.Where(i => !existingDishSubDishIds.Contains(i)).ToList();
                }
                //await _athenaDbcontext.SaveChangesAsync();

            }

            if (dishSks.Any())
            {
                foreach (int dishSk in dishSks)
                {
                    AddMenuFormDetailsReqModel addMenuFormDetailsReqModel = new AddMenuFormDetailsReqModel();
                    addMenuFormDetailsReqModel.MenuFormDate = menuFormDate;
                    addMenuFormDetailsReqModel.MenuFormWeekDate = menuFormWeekDate;
                    addMenuFormDetailsReqModel.MenuFormMealCourseSk = menuFormMealCourseSk;
                    addMenuFormDetailsReqModel.LocationMenuMapId = locationMenuMapId;
                    addMenuFormDetailsReqModel.DishSk = dishSk;
                    addMenuFormDetailsReqModel.MealTypeId = mealTypeId;
                    addMenuFormDetailsReqModel.DishMenuTypeId = dishMenuTypeId;
                    addMenuFormDetailsReqModel.UserId = userId;
                    addMenuFormDetailsReqModel.ActiveStatus = true;


                    await AddMenuFormDetails(addMenuFormDetailsReqModel);
                }
            }

            return true;
        }

        public async Task<List<MenuFormDetailsEntity>> GetMenuFormDetails(int menuFormMealCourseSk, int locationMenuMapId, DateTime menuFormDate)
        {
            List<MenuFormDetailsEntity> menuFormDetailEntities = await _athenaDbcontext.MenuFormDetailsEntity.Include(x => x.Dish)
                                                                                     .Where(x => x.MenuFormMealCourseSk == menuFormMealCourseSk 
                                                                                     && x.MenuFormDate == menuFormDate
                                                                                     && x.LocationMenuMapId == locationMenuMapId).ToListAsync();

            return menuFormDetailEntities;
        }
    }
}
