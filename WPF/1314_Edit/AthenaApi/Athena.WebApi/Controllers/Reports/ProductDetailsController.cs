
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
    public class ProductDetailsController : ControllerBase
    {

        private readonly ILogger<ProductDetailsController> _logger;
        private readonly IProductDetailsService _productDetailsService;


        public ProductDetailsController(ILogger<ProductDetailsController> logger, IProductDetailsService productDetailsService)
        {
            _logger = logger;
            _productDetailsService = productDetailsService;
        }


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Categories")]
        public object GetProductCategories()
        {
            return _productDetailsService.GetCategories();
        }

        [HttpGet("{clientId}/{fromDate}/{toDate}")]
        [SwaggerOperation(Summary = "Get Product Details")]
        public object GetCustomerOrderSummaryNoIng(DateOnly fromDate, DateOnly toDate, string Account_no = null, string orderNumber = null, string Category_Main = null)
        {
            return _productDetailsService.GetCustomerOrderSummaryNoIng(fromDate, toDate, Account_no, orderNumber, Category_Main);
        }
    }
}
