
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service.Reports
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;


        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public object GetCustomerOrderSummary(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber, string categoryMain)
        {
            return _purchaseOrderRepository.GetCustomerOrderSummary(fromDate, toDate, accountNo, orderNumber, categoryMain);
        }
    }
}
