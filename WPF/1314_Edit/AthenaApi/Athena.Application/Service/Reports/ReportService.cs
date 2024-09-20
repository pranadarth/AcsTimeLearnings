
using Athena.Application.BusinessLogic;
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service.Reports
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;


        public ReportService(IReportRepository reportRepository)
        {
            this._reportRepository = reportRepository;

        }

        public object GetReportDetails(string reportId, string clientId)
        {
            ReportMgmt reportMgmt = new ReportMgmt(_reportRepository);

            return reportMgmt.GetReportDetails(reportId, clientId);
        }
    }
}
