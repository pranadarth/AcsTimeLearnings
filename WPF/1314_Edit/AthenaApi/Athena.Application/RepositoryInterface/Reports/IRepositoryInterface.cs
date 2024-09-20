using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface.Reports
{
    public interface IReportRepository
    {
        public object GetReportDetails(string reportId, string clientId);
    }
}
