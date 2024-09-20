
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service.Reports
{
    public class SandwichDetailsService : ISandwichDetailsService
    {
        private readonly ISandwichDetailsRepository _sandwichDetailsRepository;


        public SandwichDetailsService(ISandwichDetailsRepository sandwichDetailsRepository)
        {
            _sandwichDetailsRepository = sandwichDetailsRepository;
        }

        public object GetCustomerAccounts()
        {
            return _sandwichDetailsRepository.GetCustomerAccounts();
        }

        public object GetCustomerOrderSandwiches(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber)
        {
            return _sandwichDetailsRepository.GetCustomerOrderSandwiches(fromDate, toDate, accountNo, orderNumber);
        }

        public object GetOrderNumbers()
        {
            return _sandwichDetailsRepository.GetOrderNumbers();
        }
    }
}
