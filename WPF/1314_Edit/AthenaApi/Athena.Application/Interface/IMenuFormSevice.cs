using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IMenuFormSevice
    {
        public Task<List<MenuFormTypeEntity>> GetMenuFormTypes();
        public Task<List<LocationEntity>> GetLocations(string? location);
        public Task<List<SubLocationEntity>> GetSubLocations(int locationSk);
        public Task<List<LocationDetailsModel>> GetAllLocations();

        public Task<List<CutoffTimeEntity>> GetCutOffTimings();
        public Task<object> GetMenuPlannerDashBoardData(int menuFormTypeSk, int locationSk, int? subLocationSk, DateTime startDate, DateTime endDate);

        public Task<object> CopyMenu(CopyMenuReqModel reqData);

        public Task<object> AddMenuDishes(AddMenuFormDishesReqModel reqData);

        public Task<object> UpdateMenuDishes(UpdateMenuFormDishesReqModel reqData);

        public Task<object> GetMenuFormDishes(GetMenuFormDishesReqModel reqData);

    }
}
