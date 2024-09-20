
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
    public class SalesFinancialController : ControllerBase
    {
        private readonly ILogger<SalesFinancialController> _logger;
        private readonly ISalesFinancialService _salesFinancialService;


        public SalesFinancialController(ILogger<SalesFinancialController> logger, ISalesFinancialService salesFinancialService)
        {
            _logger = logger;
            _salesFinancialService = salesFinancialService;
        }


        //[HttpPost("{clientId}")]
        //[SwaggerOperation(Summary = "Get Sales Summary Header Details")]
        //public object GetSalesSummaryHeaderDetails([FromBody] GetSalesSummaryHeaderReqModel data)
        //{
        //    return _salesFinancialService.GetSalesSummaryHeaderDetails(data.accountIds, data.months);
        //}


        [HttpGet("{clientId}/{Account_no}/{months}")]
        [SwaggerOperation(Summary = "Get Sales Summary Header Details")]
        public object GetSalesSummaryHeaderDetails(string Account_no, string months)
        {
            return _salesFinancialService.GetSalesSummaryHeaderDetails(Account_no.Split(",").ToList(), months.Split(",").ToList());
        }

        [HttpGet("{clientId}/{Account_no}/{Delivery_Account_no}/{orderDate}")]
        [SwaggerOperation(Summary = "Get Sales Summary Details")]
        public object GetSalesSummaryDetails(string Account_no, string Delivery_Account_no, DateOnly orderDate)
        {
            return _salesFinancialService.GetSalesSummaryDetails(Account_no, Delivery_Account_no, orderDate);
        }
    }
}
