using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface.Reports
{
    public interface IMealsByWardRepository
    {
        public object GetMealsList();
        public object GetHospitalsList();
        public object GetWardsList(string siteId);
        public object GetPatientMeals(DateOnly fromDate, DateOnly toDate, string meal, string siteId, string wardId);
    }
}
