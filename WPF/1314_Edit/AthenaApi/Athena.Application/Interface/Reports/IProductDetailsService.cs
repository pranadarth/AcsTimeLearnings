using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface.Reports
{
    public interface IProductDetailsService
    {
        public object GetCategories();
        public object GetCustomerOrderSummaryNoIng(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber, string categoryMain);
    }
}
