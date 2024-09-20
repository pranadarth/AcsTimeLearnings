using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface.Reports
{
    public interface IReportService
    {
        public object GetReportDetails(string reportId, string clientId);

    }
}
