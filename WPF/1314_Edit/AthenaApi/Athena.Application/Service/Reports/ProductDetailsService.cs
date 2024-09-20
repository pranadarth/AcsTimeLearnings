
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service.Reports
{
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IProductDetailsRepository _productDetailsRepository;


        public ProductDetailsService(IProductDetailsRepository productDetailsRepository)
        {
            _productDetailsRepository = productDetailsRepository;
        }

        public object GetCategories()
        {
            return _productDetailsRepository.GetProductCategories();
        }

        public object GetCustomerOrderSummaryNoIng(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber, string categoryMain)
        {
            return _productDetailsRepository.GetProductDetails(fromDate, toDate, accountNo, orderNumber, categoryMain);
        }
    }
}
