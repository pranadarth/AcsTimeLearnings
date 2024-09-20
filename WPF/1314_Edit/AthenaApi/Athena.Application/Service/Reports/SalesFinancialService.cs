
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service.Reports
{
    internal class SalesFinancialService : ISalesFinancialService
    {
        private readonly ISalesFinancialRepository _salesFinancialRepository;

        public SalesFinancialService(ISalesFinancialRepository salesFinancialRepository)
        {
            _salesFinancialRepository = salesFinancialRepository;
        }

        public object GetSalesSummaryDetails(string accountId, string deliveryAccountId, DateOnly orderedDate)
        {
            return _salesFinancialRepository.GetSalesSummaryDetails(accountId, deliveryAccountId, orderedDate);
        }

        public object GetSalesSummaryHeaderDetails(List<string> accountIds, List<string> months)
        {
            return _salesFinancialRepository.GetSalesSummaryHeaderDetails(accountIds, months);
        }
    }
}
