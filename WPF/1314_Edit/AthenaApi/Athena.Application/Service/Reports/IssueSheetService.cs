
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service.Reports
{
    public class IssueSheetService : IIssueSheetService
    {
        private readonly IIssueSheetRepository _issueSheetRepository;


        public IssueSheetService(IIssueSheetRepository issueSheetRepository)
        {
            _issueSheetRepository = issueSheetRepository;
        }

        public object GetCustomerOrderIssueSheet(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber, string categoryMain)
        {
            return _issueSheetRepository.GetCustomerOrderIssueSheet(fromDate, toDate, accountNo, orderNumber, categoryMain);
        }
    }
}
