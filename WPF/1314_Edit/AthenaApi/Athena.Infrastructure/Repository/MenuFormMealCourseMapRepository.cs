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
    public class MenuFormMealCourseMapRepository : IMenuFormMealCourseMapRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<MenuFormMealCourseMapRepository> _logger;

        public MenuFormMealCourseMapRepository(ILogger<MenuFormMealCourseMapRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<MenuFormCourseMappingModel>> GetMenuFormMealCourse(int menuFormTypeSk, int locationSk, int? subLocationSk, DateTime startDate, DateTime endDate)
        {
            var initialQuery = from mfm in _athenaDbcontext.MenuFormMealCourseMappingEntity
                               join mfd in _athenaDbcontext.MenuFormDetailsEntity
                               on mfm.MenuFormMealCourseSk equals mfd.MenuFormMealCourseSk
                               where mfd.MenuFormDate >= startDate && mfd.MenuFormDate <= endDate
                               && mfm.MenuFormTypeSk == menuFormTypeSk
                               && mfd.LocationSk == locationSk
                               && mfm.ActiveStatus == true
                               && mfd.ActiveStatus == true
                               select new
                               {
                                   MenuFormMealCourseMapping = mfm,
                                   MenuFormDetails = mfd
                               };

            var query = initialQuery.AsQueryable();

            if (subLocationSk != null)
                query = query.Where(x => x.MenuFormDetails.SubLocationSk == subLocationSk);

            return await query.Select(x => new MenuFormCourseMappingModel
            {
                MenuFormDate = x.MenuFormDetails.MenuFormDate,
                MealTypeId = x.MenuFormDetails.MealTypeId,
                MenuFormMealCourseSk = x.MenuFormDetails.MenuFormMealCourseMapping.MenuFormMealCourseSk

            }).ToListAsync();
        }

        public async Task<List<MenuFormMealsModel>> GetMenuFormMealsDetails(int menuFormTypeSk, int menuTypeSk, int mealTypeSk, int locationSk, int? subLocationSk, DateTime startDate, DateTime endDate)
        {
            var initialQuery = from mfm in _athenaDbcontext.MenuFormMealCourseMappingEntity
                               join mfd in _athenaDbcontext.MenuFormDetailsEntity
                               on mfm.MenuFormMealCourseSk equals mfd.MenuFormMealCourseSk
                               join locmap in _athenaDbcontext.LocationMenuTypeMappingEntity
                               on mfd.LocationMenuMapId equals locmap.LocMenuMapId
                               where mfd.MenuFormDate >= startDate && mfd.MenuFormDate <= endDate
                               && locmap.DishMenuTypeId == menuTypeSk
                               && mfm.MealTypeId == mealTypeSk
                               && locmap.LocationSk == locationSk
                               && mfm.MenuFormTypeSk == menuFormTypeSk
                               && mfm.ActiveStatus == true
                               && mfd.ActiveStatus == true
                               select new
                               {
                                   MenuFormMealCourseMapping = mfm,
                                   MenuFormDetails = mfd,
                                   LocationMenuMap = locmap
                               };

            var query = initialQuery.AsQueryable();

            if (subLocationSk != null)
            {
                query = query.Where(x => x.LocationMenuMap.SubLocationSk == subLocationSk);
            }

            return await query.Select(x => new MenuFormMealsModel
            {
                MenuFormTypeSk = x.MenuFormMealCourseMapping.MenuFormTypeSk,
                MealTypeId = x.MenuFormMealCourseMapping.MealTypeId,
                CourseTypeSk = x.MenuFormMealCourseMapping.CourseTypeSk,
                Display_Order = x.MenuFormMealCourseMapping.DisplayOrder,
                MenuFormDetails = x.MenuFormDetails

            }).ToListAsync();
        }

        public async Task<List<MenuFormMealsModel>> GetMenuFormCourses(int menuFormTypeSk, int menuTypeSk, int mealTypeSk, List<int> locationSks, List<int>? subLocationSks)
        {
            var initialQuery = from mfm in _athenaDbcontext.MenuFormMealCourseMappingEntity
                               join mfd in _athenaDbcontext.MenuFormDetailsEntity
                               on mfm.MenuFormMealCourseSk equals mfd.MenuFormMealCourseSk
                               join locmap in _athenaDbcontext.LocationMenuTypeMappingEntity
                               on mfd.LocationMenuMapId equals locmap.LocMenuMapId
                               where locmap.DishMenuTypeId == menuTypeSk
                               && mfm.MealTypeId == mealTypeSk
                               && locationSks.Contains(mfd.LocationSk.Value)
                               && mfm.MenuFormTypeSk == menuFormTypeSk
                               && mfm.ActiveStatus == true
                               && mfd.ActiveStatus == true
                               select new
                               {
                                   MenuFormMealCourseMapping = mfm,
                                   MenuFormDetails = mfd,
                                   LocationMenuMap = locmap
                               };
            var query = initialQuery.AsQueryable();

            if (subLocationSks != null)
            {
                query = query.Where(x => subLocationSks.Contains(x.LocationMenuMap.SubLocationSk.Value));
            }

            return await query.Select(x => new MenuFormMealsModel
            {
                CourseTypeSk = x.MenuFormMealCourseMapping.CourseTypeSk,
                MenuFormDetails = x.MenuFormDetails,
                LocationSk = x.LocationMenuMap.LocationSk,
                SubLocationSk = x.LocationMenuMap.SubLocationSk,
            }).ToListAsync();
        }

        public async Task<int> AddMenuFormMealCourseMap(AddMenuFormCourseMealMapReqData addMenuFormCourseMealMapReqData)
        {
            MenuFormMealCourseMappingEntity newMap = new MenuFormMealCourseMappingEntity()
            {
                MealTypeId = addMenuFormCourseMealMapReqData.MealTypeId,
                MenuFormTypeSk = addMenuFormCourseMealMapReqData.MenuFormTypeSk,
                CourseTypeSk = addMenuFormCourseMealMapReqData.CourseTypeSk,
                DisplayOrder = addMenuFormCourseMealMapReqData.DisplayOrder,
                ActiveStatus = true,
                CreatedBy = addMenuFormCourseMealMapReqData.UserId,
                CreatedDate = DateTime.UtcNow

            };

            await _athenaDbcontext.MenuFormMealCourseMappingEntity.AddAsync(newMap);
            await _athenaDbcontext.SaveChangesAsync();

            return newMap.MenuFormMealCourseSk;
        }


        public async Task<MenuFormMealCourseMappingEntity> GetMenuFormMealCourseMap(int menuFormTypeId, int mealTypeId, int courseTypeId)
        {

            MenuFormMealCourseMappingEntity menuFormMealCourseMappingEntity = await _athenaDbcontext.MenuFormMealCourseMappingEntity.Where(x => x.MenuFormTypeSk == menuFormTypeId
                                                                            && x.MealTypeId == mealTypeId
                                                                            && x.CourseTypeSk == courseTypeId).SingleOrDefaultAsync();

            return menuFormMealCourseMappingEntity;
        }

        public async Task<List<MenuFormMealCourseMappingEntity>> GetMenuFormMealCourseMap(int menuFormTypeId, int mealTypeId)
        {
            var query = _athenaDbcontext.MenuFormMealCourseMappingEntity.Include(x => x.CourseType).AsQueryable();
            query  = query.Where(x => x.MenuFormTypeSk == menuFormTypeId && x.MealTypeId == mealTypeId);

            return await query.ToListAsync();
        }
    }
}
