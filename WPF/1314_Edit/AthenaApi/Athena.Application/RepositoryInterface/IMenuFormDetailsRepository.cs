using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IMenuFormDetailsRepository
    {
        public Task<int> AddMenuFormDetails(AddMenuFormDetailsReqModel addMenuFormDetailsReqModel);

        public Task<bool> UpdateMenuFormDetails(int menuFormMealCourseSk, List<int> dishSks, string userId, DateTime menuFormDate, DateTime menuFormWeekDate, int locationMenuMapId, int mealTypeId, int dishMenuTypeId, int quantity, bool activeStatus);

        public Task<List<MenuFormDetailsEntity>> GetMenuFormDetails(int menuFormMealCourseSk, int locationMenuMapId, DateTime menuFormDate);
    }
}
