﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface.Reports
{
    public interface ISandwichDetailsService
    {
        public object GetCustomerAccounts();
        public object GetOrderNumbers();

        public object GetCustomerOrderSandwiches(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber);

    }
}
