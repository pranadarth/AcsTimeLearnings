
using Asp.Versioning;
using Athena.Application.Interface.Reports;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athena.WebApi.Controllers.Reports
{
    [ApiVersion("10.0")]
    [Route("apiv{version:apiVersion}[controller]/[action]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ILogger<PurchaseOrderController> _logger;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IProductDetailsService _productDetailsService;
        private readonly ISandwichDetailsService _sandwichDetailsService;

        public PurchaseOrderController(ILogger<PurchaseOrderController> logger, IPurchaseOrderService purchaseOrderService, IProductDetailsService productDetailsService, ISandwichDetailsService sandwichDetailsService)
        {
            _logger = logger;
            _purchaseOrderService = purchaseOrderService;
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

        [HttpGet("{clientId}/{fromDate}/{toDate}")]
        [SwaggerOperation(Summary = "Get Customer Order Summary")]
        public object GetCustomerOrderSummary(DateOnly fromDate, DateOnly toDate, string accountNo = null, string orderNumber = null, string categoryMain = null)
        {
            return _purchaseOrderService.GetCustomerOrderSummary(fromDate, toDate, accountNo, orderNumber, categoryMain);
        }

        [HttpGet("{clientId}/{fromDate}/{toDate}")]
        [SwaggerOperation(Summary = "Get Order Details")]
        public object GetCustomerOrderSummaryNoIng(DateOnly fromDate, DateOnly toDate, string Account_No = null, string orderNumber = null, string Category_Main = null)
        {
            return _productDetailsService.GetCustomerOrderSummaryNoIng(fromDate, toDate, Account_No, orderNumber, Category_Main);
        }
    }
}
