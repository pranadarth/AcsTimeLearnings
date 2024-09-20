using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface.Reports
{
    public interface IPurchaseOrderRepository
    {
        public object GetCustomerOrderSummary(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber, string categoryMain);
    }
}
