using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class GetSalesSummaryHeaderReqModel
    {
        public List<string> accountIds { get; set; }
        public List<string> months { get; set; }
    }
}
