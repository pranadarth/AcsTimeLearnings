using Athena.Application.RepositoryInterface;
using Athena.Application.RepositoryInterface.Reports;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.BusinessLogic
{
    public class MenuFormMgmt
    {
        private readonly IMenuFormMealCourseMapRepository _iMenuFormMealCourseMapRepository;
        private readonly IDishMealTypeRepository _iDishMealRepository;
        private readonly IMenuFormDetailsRepository _iMenuFormDetailsRepository;
        private readonly ILocationMenuTypeMappingRepository _iLocationMenuTypeMappingRepository;
        private readonly IMenuFormAllowanceRepository _iMenuFormAllowanceRepository;

        public MenuFormMgmt(IMenuFormMealCourseMapRepository iMenuFormMealCourseMapRepository, IDishMealTypeRepository iDishMealRepository,
            IMenuFormDetailsRepository iMenuFormDetailsRepository, ILocationMenuTypeMappingRepository iLocationMenuTypeMappingRepository, IMenuFormAllowanceRepository imenuFormAllowanceRepository)
        {
            _iMenuFormMealCourseMapRepository = iMenuFormMealCourseMapRepository;
            _iDishMealRepository = iDishMealRepository;
            _iMenuFormDetailsRepository = iMenuFormDetailsRepository;
            _iLocationMenuTypeMappingRepository = iLocationMenuTypeMappingRepository;
            _iMenuFormAllowanceRepository = imenuFormAllowanceRepository;
        }

        public async Task<object> GetMenuPlannerDashboardData(int menuFormTypeSk, int locationSk, int? subLocationSk, DateTime startDate, DateTime endDate)
        {
            //TODO Optimize
            List<MenuFormCourseMappingModel> menuCourseDet = await _iMenuFormMealCourseMapRepository.GetMenuFormMealCourse(menuFormTypeSk, locationSk, subLocationSk, startDate, endDate);
            if (menuCourseDet != null)
            {
                List<DishMealTypeEntity> mealTypes = await _iDishMealRepository.GetDishMealTypes();
                List<DateTime> dates = GetDatesInRange(startDate, endDate);

                if (mealTypes != null)
                {
                    var mealTypesObj = mealTypes.Select(m => new
                    {
                        MealType = m.MealTypeCode,
                        MealTypeId = m.MealTypeId,
                        Dates = dates.Select(d => new
                        {
                            Date = d.Date.Date,
                            IsPlanned = menuCourseDet.Where(mcd => mcd.MenuFormDate == d.Date && mcd.MealTypeId == m.MealTypeId).Select(x => true).FirstOrDefault()
                        }).ToList()


                    }).ToList();

                    return mealTypesObj;
                }
            }
            return null;
        }

        public async Task<object> CopyMenu(CopyMenuReqModel reqData)
        {
            List<CopyMenuResponeModel> responeJson = new List<CopyMenuResponeModel>();


            List<MenuFormMealsModel> copyFromMenuDishes = await _iMenuFormMealCourseMapRepository.GetMenuFormMealsDetails(reqData.MenuFormTypeId, reqData.CopyFrom.MenuTypeId, reqData.CopyFrom.MealTypeId, reqData.CopyFrom.LocationSk, reqData.CopyFrom.SubLocationSk, reqData.CopyFrom.FromDate, reqData.CopyFrom.ToDate);
            if (copyFromMenuDishes != null && copyFromMenuDishes.Count > 0)
            {
                List<int> copyFromCourseTypeIds = copyFromMenuDishes.Where(x => x.CourseTypeSk != null).Select(x => x.CourseTypeSk.Value).ToList();
                if (copyFromCourseTypeIds != null && copyFromCourseTypeIds.Count > 0)
                {
                    List<MenuFormMealsModel> copyToCourses = await _iMenuFormMealCourseMapRepository.GetMenuFormCourses(reqData.MenuFormTypeId, reqData.CopyTo.MenuTypeId, reqData.CopyTo.MealTypeId, reqData.CopyTo.LocationSks, reqData.CopyTo.SubLocationSks);

                    //Selected menu type will not be in all select location so filter the menu type which contains in location
                    //Example Location Main store and Wards, Menu type vegan, vegan will not be there in both location in this scenario show
                    //to user location does not have menu type

                    //Location
                    List<int> locMenuMapIds = copyToCourses.Select(d => d.MenuFormDetails.LocationMenuMapId.Value).Distinct().ToList();
                    List<int> copyToLocSks = copyToCourses.Select(d => d.LocationSk.Value).Distinct().ToList();
                    //To Show in frontend
                    List<int> locIdsDoesNotHaveMenuType = reqData.CopyTo.LocationSks.Where(l => !copyToLocSks.Contains(l)).ToList();

                    List<CopyMenuResponeModel> locMisMatch = locIdsDoesNotHaveMenuType.Select(x => new CopyMenuResponeModel
                    {
                        LocationSk = x,
                        IsLocationMenuTypeMismatch = true,
                    }).ToList();

                    responeJson.AddRange(locMisMatch);

                    foreach (int locMenuMapId in locMenuMapIds)
                    {
                        List<MenuFormMealsModel> copyToCourseDetails = copyToCourses.Where(x => x.MenuFormDetails != null && x.MenuFormDetails.LocationMenuMapId == locMenuMapId).ToList();
                        if (copyToCourseDetails != null && copyToCourseDetails.Count > 0)
                        {
                            List<int> courses = copyToCourseDetails.Where(x => x.CourseTypeSk != null).Select(x => x.CourseTypeSk.Value).ToList();

                            bool areEqual = copyFromCourseTypeIds.GroupBy(x => x).All(g => g.Count() == courses.Count(x => x == g.Key)) && courses.GroupBy(x => x).All(g => g.Count() == copyFromCourseTypeIds.Count(x => x == g.Key));
                            if (areEqual)
                            {
                                List<DateTime> copyFromDateRange = GetDatesInRange(reqData.CopyFrom.FromDate, reqData.CopyFrom.ToDate);
                                List<DateTime> copyToDateRange = GetDatesInRange(reqData.CopyTo.FromDate, reqData.CopyTo.ToDate);

                                List<DateDetails> dateDetails = new List<DateDetails>();
                                for (int i = 0; i < copyFromDateRange.Count; i++)
                                {
                                    DateTime copyFromDate = copyFromDateRange[i];
                                    DateTime copyToDate = copyToDateRange[i];

                                    List<MenuFormDetailsEntity> copyFromDishDetails = copyFromMenuDishes.Select(d => d.MenuFormDetails).ToList();
                                    if (copyFromDishDetails != null && copyFromDishDetails.Count > 0)
                                    {
                                        List<MenuFormMealsModel> copyFromMenuDateDishes = copyFromMenuDishes.Where(x => x.MenuFormDetails.MenuFormDate == copyFromDate).ToList();
                                        if (copyFromMenuDateDishes != null && copyFromMenuDateDishes.Count > 0)
                                        {
                                            foreach (MenuFormMealsModel dish in copyFromMenuDateDishes)
                                            {
                                                MenuFormMealCourseMappingEntity menuFormMealCourseMap = await _iMenuFormMealCourseMapRepository.GetMenuFormMealCourseMap(reqData.MenuFormTypeId, reqData.CopyTo.MealTypeId, dish.CourseTypeSk.Value);
                                                if (menuFormMealCourseMap != null)
                                                {
                                                    AddMenuFormDetailsReqModel addMenuFormDetailsReqModel = new AddMenuFormDetailsReqModel();
                                                    addMenuFormDetailsReqModel.MenuFormDate = copyToDate;
                                                    addMenuFormDetailsReqModel.MenuFormWeekDate = DateTime.UtcNow;
                                                    addMenuFormDetailsReqModel.MenuFormMealCourseSk = menuFormMealCourseMap.MenuFormMealCourseSk;
                                                    addMenuFormDetailsReqModel.LocationMenuMapId = locMenuMapId;
                                                    addMenuFormDetailsReqModel.DishSk = dish.MenuFormDetails.DishSk;
                                                    addMenuFormDetailsReqModel.MealTypeId = reqData.CopyTo.MealTypeId;
                                                    addMenuFormDetailsReqModel.DishMenuTypeId = reqData.CopyTo.MenuTypeId;
                                                    addMenuFormDetailsReqModel.UserId = reqData.UserId;
                                                    addMenuFormDetailsReqModel.ActiveStatus = true;

                                                    await _iMenuFormDetailsRepository.AddMenuFormDetails(addMenuFormDetailsReqModel);
                                                }
                                                else
                                                {
                                                    CopyMenuResponeModel exist = responeJson.Where(x => x.CourseTypeSk == dish.CourseTypeSk.Value).SingleOrDefault();
                                                    if (exist is null)
                                                    {
                                                        CopyMenuResponeModel responeModel = new CopyMenuResponeModel();
                                                        responeModel.LocationSk = locMenuMapId;
                                                        responeModel.CourseTypeSk = dish.CourseTypeSk.Value;
                                                        responeModel.IsMenuFormMealCourseMismatch = true;
                                                        responeModel.IsCourseMismatch = false;
                                                        responeModel.DateDetails = dateDetails;

                                                        responeJson.Add(responeModel);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    DateDetails dateDetail = new DateDetails();
                                    dateDetail.Date = copyToDate;
                                    dateDetail.IsSuccess = true;
                                    dateDetail.MFDetailsSk = 0;
                                }

                                CopyMenuResponeModel copyMenuResponeModel = new CopyMenuResponeModel();
                                copyMenuResponeModel.LocationSk = locMenuMapId;
                                copyMenuResponeModel.IsLocationMenuTypeMismatch = false;
                                copyMenuResponeModel.IsCourseMismatch = false;
                                copyMenuResponeModel.DateDetails = dateDetails;

                                responeJson.Add(copyMenuResponeModel);
                            }
                            else
                            {
                                CopyMenuResponeModel copyMenuResponeModel = new CopyMenuResponeModel();
                                copyMenuResponeModel.LocationSk = locMenuMapId;
                                copyMenuResponeModel.IsCourseMismatch = true;

                                responeJson.Add(copyMenuResponeModel);
                            }
                        }
                    }
                }
            }

            return responeJson;
        }

        public static List<DateTime> GetDatesInRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;
        }

        public static DateTime GetSpecificWeekday(DateTime date, DayOfWeek targetDay)
        {
            int daysToSubtract = (int)date.DayOfWeek - (int)targetDay;
            if (daysToSubtract < 0)
            {
                daysToSubtract += 7;
            }
            return date.AddDays(-daysToSubtract);
        }

        public async Task<bool> AddMenuDishes(AddMenuFormDishesReqModel reqData)
        {
            foreach (CourseDetails course in reqData.CourseDetails)
            {
                MenuFormMealCourseMappingEntity menuFormMealCourseMap = await _iMenuFormMealCourseMapRepository.GetMenuFormMealCourseMap(reqData.MenuFormTypeSk, reqData.MealTypeSk, course.CourseTypeSk);
                if (menuFormMealCourseMap == null)
                    continue;

                LocationMenuTypeMappingEntity locationMenuTypeMappingEntity = await _iLocationMenuTypeMappingRepository.GetMenuTypeLocationMap(reqData.MenuTypeSk, reqData.LocationSk, reqData.SubLocationSk);
                if (locationMenuTypeMappingEntity is not null)
                {
                    await _iMenuFormAllowanceRepository.AddMenuFormAllowance(course.Allowance, menuFormMealCourseMap.MenuFormMealCourseSk, true, reqData.UserId);

                    foreach (int dishSk in course.DishSk)
                    {
                        AddMenuFormDetailsReqModel addMenuFormDetailsReqModel = new AddMenuFormDetailsReqModel();
                        addMenuFormDetailsReqModel.MenuFormDate = reqData.Date;
                        addMenuFormDetailsReqModel.MenuFormWeekDate = GetSpecificWeekday(reqData.Date, DayOfWeek.Monday);
                        addMenuFormDetailsReqModel.MenuFormMealCourseSk = menuFormMealCourseMap.MenuFormMealCourseSk;
                        addMenuFormDetailsReqModel.LocationMenuMapId = locationMenuTypeMappingEntity.LocMenuMapId;
                        addMenuFormDetailsReqModel.DishSk = dishSk;
                        addMenuFormDetailsReqModel.MealTypeId = reqData.MealTypeSk;
                        addMenuFormDetailsReqModel.DishMenuTypeId = reqData.MenuTypeSk;
                        addMenuFormDetailsReqModel.UserId = reqData.UserId;
                        addMenuFormDetailsReqModel.ActiveStatus = true;
                        await _iMenuFormDetailsRepository.AddMenuFormDetails(addMenuFormDetailsReqModel);
                    }
                }
            }
            return true;
        }

        public async Task<bool> UpdateMenuDishes(UpdateMenuFormDishesReqModel reqData)
        {
            foreach (CourseDetails course in reqData.CourseDetails)
            {
                MenuFormMealCourseMappingEntity menuFormMealCourseMap = await _iMenuFormMealCourseMapRepository.GetMenuFormMealCourseMap(reqData.MenuFormTypeSk, reqData.MealTypeSk, course.CourseTypeSk);
                if (menuFormMealCourseMap == null)
                    continue;

                LocationMenuTypeMappingEntity locationMenuTypeMappingEntity = await _iLocationMenuTypeMappingRepository.GetMenuTypeLocationMap(reqData.MenuTypeSk, reqData.LocationSk, reqData.SubLocationSk);
                if (locationMenuTypeMappingEntity is not null)
                {
                    await _iMenuFormAllowanceRepository.UpdateMenuFormAllowance(course.Allowance, menuFormMealCourseMap.MenuFormMealCourseSk, reqData.ActiveStatus, reqData.UserId);

                    await _iMenuFormDetailsRepository.UpdateMenuFormDetails(menuFormMealCourseMap.MenuFormMealCourseSk, course.DishSk,
                       reqData.UserId, reqData.Date, GetSpecificWeekday(reqData.Date, DayOfWeek.Monday), locationMenuTypeMappingEntity.LocMenuMapId
                       , reqData.MealTypeSk, reqData.MenuTypeSk, 0, reqData.ActiveStatus);
                }
            }
            return true;
        }

        public async Task<object> GetMenuFormDishes(GetMenuFormDishesReqModel reqData)
        {
            List<MenuFormDishesModel> reponseJson = new List<MenuFormDishesModel>();

            List<MenuFormMealCourseMappingEntity> menuFormMealCourseMappingEntities = await _iMenuFormMealCourseMapRepository.GetMenuFormMealCourseMap(reqData.MenuFormTypeSk, reqData.MealTypeId);

            LocationMenuTypeMappingEntity locationMenuTypeMappingEntity = await _iLocationMenuTypeMappingRepository.GetMenuTypeLocationMap(reqData.MenuTypeSk, reqData.LocationSk, reqData.SubLocationSk);

            foreach (MenuFormMealCourseMappingEntity menuFormMealCourseMap in menuFormMealCourseMappingEntities)
            {
                MenuFormDishesModel menuFormDishesModel = new MenuFormDishesModel();
                menuFormDishesModel.CourseTypeSk = (int)menuFormMealCourseMap.CourseTypeSk;
                menuFormDishesModel.CourseName = menuFormMealCourseMap.CourseType.CourseCode;
                MenuFormAllowanceEntity menuFormAllowance = await _iMenuFormAllowanceRepository.GetMenuFormAllowance(menuFormMealCourseMap.MenuFormMealCourseSk);
                if (menuFormAllowance != null)
                {
                    menuFormDishesModel.Allowance = menuFormAllowance.AllowanceQty;
                }

                if (locationMenuTypeMappingEntity is not null)
                {
                    List<MenuFormDetailsEntity> menuFormDetailsEntities = await _iMenuFormDetailsRepository.GetMenuFormDetails(menuFormMealCourseMap.MenuFormMealCourseSk, locationMenuTypeMappingEntity.LocMenuMapId, reqData.Date);

                    if (menuFormDetailsEntities.Any())
                    {
                        menuFormDishesModel.Dish = menuFormDetailsEntities
                            .Where(x => x.Dish != null)
                            .Select(x => new DishMenuFormModel
                            {
                                DishName = !string.IsNullOrEmpty(x.Dish.DishName) ? x.Dish.DishName : null,
                                DishSk = x.Dish?.DishSk ?? -1,
                                Dish_Image = !string.IsNullOrEmpty(x.Dish.DishImage) ? x.Dish.DishImage : null,
                                Display_Name = !string.IsNullOrEmpty(x.Dish.DisplayName) ? x.Dish.DisplayName : null,
                                Display_Description = !string.IsNullOrEmpty(x.Dish.DishDescription) ? x.Dish.DishDescription : null,
                                cost = x.Dish?.Cost ?? -1,
                                cost_per_portion = x.Dish?.CostPerPortion ?? -1,
                                sale_price = x.Dish?.SalePrice ?? -1
                            }).ToList();
                    }

                }
                reponseJson.Add(menuFormDishesModel);
            }
            return reponseJson;
        }
    }

}