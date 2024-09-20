
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
    public class IssueSheetController : ControllerBase
    {
        private readonly ILogger<IssueSheetController> _logger;
        private readonly IIssueSheetService _issueSheetService;
        private readonly IProductDetailsService _productDetailsService;
        private readonly ISandwichDetailsService _sandwichDetailsService;

        public IssueSheetController(ILogger<IssueSheetController> logger, IIssueSheetService issueSheetService, IProductDetailsService productDetailsService, ISandwichDetailsService sandwichDetailsService)
        {
            _logger = logger;
            _issueSheetService = issueSheetService;
            _productDetailsService = productDetailsService;
            _sandwichDetailsService = sandwichDetailsService;
        }


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Categories")]
        public object GetCategories()
        {
            return _productDetailsService.GetCategories();
        }

        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Customer Accounts")]
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

        [HttpGet("{clientId}/{fromDate}/{toDate}/{accountNo}/{orderNumber}/{categoryMain}")]
        [SwaggerOperation(Summary = "Get Customer Order Issue Sheet")]
        public object GetCustomerOrderIssueSheet(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber, string categoryMain)
        {
            return _issueSheetService.GetCustomerOrderIssueSheet(fromDate, toDate, accountNo, orderNumber, categoryMain);
        }

        [HttpGet("{clientId}/{fromDate}/{toDate}")]
        [SwaggerOperation(Summary = "Get Order Details")]
        public object GetCustomerOrderSummaryNoIng(DateOnly fromDate, DateOnly toDate, string Account_No = null, string orderNumber = null, string Category_Main = null)
        {
            return _productDetailsService.GetCustomerOrderSummaryNoIng(fromDate, toDate, Account_No, orderNumber, Category_Main);
        }
    }
}
