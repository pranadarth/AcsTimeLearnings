using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IMenuFormMealCourseMapRepository
    {
        public Task<List<MenuFormCourseMappingModel>> GetMenuFormMealCourse(int menuFormTypeSk, int locationSk, int? subLocationSk, DateTime startDate, DateTime endDate);
        public Task<List<MenuFormMealsModel>> GetMenuFormMealsDetails(int menuFormTypeSk, int menuTypeSk, int mealTypeSk, int locationSk, int? subLocationSk, DateTime startDate, DateTime endDate);
        public Task<List<MenuFormMealsModel>> GetMenuFormCourses(int menuFormTypeSk, int menuTypeSk, int mealTypeSk, List<int> locationSks, List<int>? subLocationSks);
        public Task<int> AddMenuFormMealCourseMap(AddMenuFormCourseMealMapReqData addMenuFormCourseMealMapReqData);
        public Task<MenuFormMealCourseMappingEntity> GetMenuFormMealCourseMap(int menuFormTypeId, int mealTypeId, int courseTypeId);
        public Task<List<MenuFormMealCourseMappingEntity>> GetMenuFormMealCourseMap(int menuFormTypeId, int mealTypeId);
    }
}

