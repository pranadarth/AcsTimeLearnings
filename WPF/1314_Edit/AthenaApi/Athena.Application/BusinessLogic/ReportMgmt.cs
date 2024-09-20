using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.BusinessLogic
{
    public class ReportMgmt
    {
        private readonly IReportRepository _reportRepository;

        public ReportMgmt(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public object GetReportDetails(string reportId, string clientId)
        {
            _reportRepository.GetReportDetails(reportId, clientId);

            return null;
        }
    }
}
