
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service.Reports
{
    public class MealByWardService : IMealsByWardService
    {
        private readonly IMealsByWardRepository _mealsByWardRepository;


        public MealByWardService(IMealsByWardRepository mealsByWardRepository)
        {
            _mealsByWardRepository = mealsByWardRepository;
        }

        public object GetHospitalsList()
        {
            return _mealsByWardRepository.GetHospitalsList();
        }

        public object GetMealsList()
        {
            return _mealsByWardRepository.GetMealsList();
        }

        public object GetPatientMeals(DateOnly fromDate, DateOnly toDate, string meal, string siteId, string wardId)
        {
            return _mealsByWardRepository.GetPatientMeals(fromDate, toDate, meal, siteId, wardId);
        }

        public object GetWardsList(string siteId)
        {
            return _mealsByWardRepository.GetWardsList(siteId);
        }
    }
}
