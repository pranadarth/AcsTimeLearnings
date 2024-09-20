using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface.Reports
{
    public interface ISalesFinancialService
    {
        public object GetSalesSummaryHeaderDetails(List<string> accountIds, List<string> months);
        public object GetSalesSummaryDetails(string accountId, string deliveryAccountId, DateOnly orderedDate);
    }
}
