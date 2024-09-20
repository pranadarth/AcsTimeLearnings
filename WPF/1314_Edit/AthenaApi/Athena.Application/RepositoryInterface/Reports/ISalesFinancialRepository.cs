using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface.Reports
{
    public interface ISalesFinancialRepository
    {
        public object GetSalesSummaryHeaderDetails(List<string> accountIds, List<string> months);
        public object GetSalesSummaryDetails(string accountId, string deliveryAccountId, DateOnly orderedDate);
    }
}
