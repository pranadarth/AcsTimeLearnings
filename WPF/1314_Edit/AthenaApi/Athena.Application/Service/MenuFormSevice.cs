using Athena.Application.BusinessLogic;
using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class MenuFormSevice : IMenuFormSevice
    {
        private readonly IMenuFormTypesRepository _iMenuFormTypesRepository;
        private readonly ILocationRepository _iLocationRepository;
        private readonly ISubLocationRepository _iSubLocationRepository;
        private readonly ICutoffTimeRepository _cutoffTimeRepository;
        private readonly MenuFormMgmt _menuFormMgmt;
        private readonly IMenuFormMealCourseMapRepository _iMenuFormMealCourseMapRepository;
        private readonly IDishMealTypeRepository _iDishMealRepository;
        private readonly IMenuFormDetailsRepository _iMenuFormDetailsRepository;
        private readonly ILocationMenuTypeMappingRepository _iLocationMenuTypeMappingRepository;
        private readonly IMenuFormAllowanceRepository _iMenuFormAllowanceRepository;

        public MenuFormSevice(IMenuFormTypesRepository iMenuFormTypesRepository, ILocationRepository iLocationRepository,
            ISubLocationRepository iSubLocationRepository, ICutoffTimeRepository cutoffTimeRepository,
            IMenuFormMealCourseMapRepository iMenuFormMealCourseMapRepository, IDishMealTypeRepository iDishMealRepository,
            IMenuFormDetailsRepository iMenuFormDetailsRepository, ILocationMenuTypeMappingRepository iLocationMenuTypeMappingRepository, IMenuFormAllowanceRepository iMenuFormAllowanceRepository)
        {
            _iMenuFormTypesRepository = iMenuFormTypesRepository;
            _iLocationRepository = iLocationRepository;
            _iSubLocationRepository = iSubLocationRepository;
            _cutoffTimeRepository = cutoffTimeRepository;
            _iMenuFormMealCourseMapRepository = iMenuFormMealCourseMapRepository;
            _iDishMealRepository = iDishMealRepository;
            _iMenuFormDetailsRepository = iMenuFormDetailsRepository;
            _iLocationMenuTypeMappingRepository = iLocationMenuTypeMappingRepository;
            _iMenuFormAllowanceRepository = iMenuFormAllowanceRepository;
            _menuFormMgmt = new MenuFormMgmt(iMenuFormMealCourseMapRepository, iDishMealRepository, _iMenuFormDetailsRepository, _iLocationMenuTypeMappingRepository, _iMenuFormAllowanceRepository);
        }

        public async Task<List<CutoffTimeEntity>> GetCutOffTimings()
        {
            return await _cutoffTimeRepository.GetCutOffTimings();
        }

        public async Task<List<LocationEntity>> GetLocations(string? location)
        {
            return await _iLocationRepository.GetLocations(location);
        }

        public async Task<List<MenuFormTypeEntity>> GetMenuFormTypes()
        {
            return await _iMenuFormTypesRepository.GetMenuFormTypes();
        }

        public async Task<List<SubLocationEntity>> GetSubLocations(int locationSk)
        {
            return await _iSubLocationRepository.GetSubLocations(locationSk);
        }

        public async Task<object> GetMenuPlannerDashBoardData(int menuFormTypeSk, int locationSk, int? subLocationSk, DateTime startDate, DateTime endDate)
        {
            return await _menuFormMgmt.GetMenuPlannerDashboardData(menuFormTypeSk, locationSk, subLocationSk, startDate, endDate);
        }

        public async Task<object> CopyMenu(CopyMenuReqModel reqData)
        {
            return await _menuFormMgmt.CopyMenu(reqData);
        }
        public async Task<object> AddMenuDishes(AddMenuFormDishesReqModel reqData)
        {
            return await _menuFormMgmt.AddMenuDishes(reqData);
        }

        public async Task<object> UpdateMenuDishes(UpdateMenuFormDishesReqModel reqData)
        {
            return await _menuFormMgmt.UpdateMenuDishes(reqData);
        }

        public async Task<object> GetMenuFormDishes(GetMenuFormDishesReqModel reqData)
        {
            return  await _menuFormMgmt.GetMenuFormDishes(reqData);
        }


        public async Task<List<LocationDetailsModel>?> GetAllLocations()
        {
            List<LocationEntity> locations = await _iLocationRepository.GetLocations(null);
            if (locations is not null && locations.Count > 0)
            {
                List<SubLocationEntity> subLocations = await _iSubLocationRepository.GetAllSubLocations();

                if (subLocations is not null)
                {
                    List<LocationDetailsModel> locationDetails = locations.Select(l => new LocationDetailsModel
                    {
                        DisplayOrder = l.DisplayOrder,
                        LocationCode = l.LocationCode,
                        LocationDesc = l.LocationDesc,
                        LocationSk = l.LocationSk,
                        SubLocationDetails = subLocations.Where(s => s.LocationSk == l.LocationSk).Select(s => new SubLocationDetailsModel
                        {
                            LocationSk = s.LocationSk,
                            SubLocationCode = s.SubLocationCode,
                            DisplayOrder = s.DisplayOrder,
                            SubLocationDesc = s.SubLocationDesc,
                            SubLocationSk = s.SubLocationSk
                        }).ToList()

                    }).ToList();

                    return locationDetails;
                }
            }
            return null;
        }
    }
}
