using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface.Reports
{
    public interface IProductDetailsRepository
    {
        public object GetProductCategories();
        public object GetProductDetails(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber, string categoryMain);
    }
}
