﻿
using Athena.Application.RepositoryInterface.Reports;
using Athena.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository.Reports
{
    public class SandwichDetailsRepository : ISandwichDetailsRepository
    {

        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<SandwichDetailsRepository> _logger;

        public SandwichDetailsRepository(ILogger<SandwichDetailsRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public object GetCustomerAccounts()
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "select * from [dbo].[vw_RPT_customer_Account_Filter]";

                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    return JsonConvert.SerializeObject(dataTable);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

            return null;
        }

        public object GetCustomerOrderSandwiches(DateOnly fromDate, DateOnly toDate, string accountNo, string orderNumber)
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "[sp_RPT_customer_orders_sandwiches_A]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter fromDateParameter = new SqlParameter();
                fromDateParameter.ParameterName = "@fromDate";
                fromDateParameter.SqlDbType = SqlDbType.Date;
                fromDateParameter.Value = fromDate;
                cmd.Parameters.Add(fromDateParameter);

                SqlParameter toDateParameter = new SqlParameter();
                toDateParameter.ParameterName = "@toDate";
                toDateParameter.SqlDbType = SqlDbType.Date;
                toDateParameter.Value = toDate;
                cmd.Parameters.Add(toDateParameter);

                if (!string.IsNullOrWhiteSpace(accountNo))
                {
                    SqlParameter accountNoParameter = new SqlParameter();
                    accountNoParameter.ParameterName = "@accountNo";
                    accountNoParameter.SqlDbType = SqlDbType.VarChar;
                    accountNoParameter.Value = accountNo;
                    cmd.Parameters.Add(accountNoParameter);
                }

                if (!string.IsNullOrWhiteSpace(orderNumber))
                {
                    SqlParameter orderNumberParameter = new SqlParameter();
                    orderNumberParameter.ParameterName = "@orderNumber";
                    orderNumberParameter.SqlDbType = SqlDbType.VarChar;
                    orderNumberParameter.Value = orderNumber;
                    cmd.Parameters.Add(orderNumberParameter);
                }

                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    return JsonConvert.SerializeObject(dataTable);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

            return null;
        }

        public object GetOrderNumbers()
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "select * from [dbo].[vw_RPT_Order_Number_Filter]";

                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    return JsonConvert.SerializeObject(dataTable);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }

            return null;
        }
    }
}
