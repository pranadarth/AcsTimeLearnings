
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
    public class SandwichDetailsController : ControllerBase
    {
        private readonly ILogger<SandwichDetailsController> _logger;
        private readonly ISandwichDetailsService _sandwichDetailsService;


        public SandwichDetailsController(ILogger<SandwichDetailsController> logger, ISandwichDetailsService sandwichDetailsService)
        {
            _logger = logger;
            _sandwichDetailsService = sandwichDetailsService;
        }


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Custom Accounts")]
        public object GetCustomerAccounts()
        {
            return _sandwichDetailsService.GetCustomerAccounts();
        }

        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get order Numbers")]
        public object GetOrderNumbers()
        {
            return _sandwichDetailsService.GetOrderNumbers();
        }

        [HttpGet("{clientId}/{fromDate}/{toDate}")]
        [SwaggerOperation(Summary = "Get Customer ordered Sandwiches")]
        public object GetCustomerOrderSandwiches(DateOnly fromDate, DateOnly toDate, string Account_no = null, string Order_number = null)
        {
            return _sandwichDetailsService.GetCustomerOrderSandwiches(fromDate, toDate, Account_no, Order_number);
        }
    }
}
