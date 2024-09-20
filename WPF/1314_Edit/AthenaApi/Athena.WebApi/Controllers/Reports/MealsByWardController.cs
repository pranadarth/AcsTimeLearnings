
using Asp.Versioning;
using Athena.Application.Interface.Reports;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athena.WebApi.Controllers.Reports
{
    [ApiVersion("10.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class MealsByWardController : ControllerBase
    {
        private readonly ILogger<MealsByWardController> _logger;
        private readonly IMealsByWardService _mealsByWardService;


        public MealsByWardController(ILogger<MealsByWardController> logger, IMealsByWardService mealsByWardService)
        {
            _logger = logger;
            _mealsByWardService = mealsByWardService;
        }


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Meals List")]
        public object GetMeals()
        {
            return _mealsByWardService.GetMealsList();
        }

        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Hospital List")]
        public object GetHospitalsList()
        {
            return _mealsByWardService.GetHospitalsList();
        }

        [HttpGet("{clientId}/{siteId}")]
        [SwaggerOperation(Summary = "Get Ward List")]
        public object GetWardsList(string siteId)
        {
            return _mealsByWardService.GetWardsList(siteId);
        }

        [HttpGet("{clientId}/{fromDate}/{toDate}")]
        [SwaggerOperation(Summary = "Get Patient Meals")]
        public object GetPatientMeals(DateOnly fromDate, DateOnly toDate, string meal = null, string siteId = null, string wardId = null)
        {
            return _mealsByWardService.GetPatientMeals(fromDate, toDate, meal, siteId, wardId);
        }
    }
}
