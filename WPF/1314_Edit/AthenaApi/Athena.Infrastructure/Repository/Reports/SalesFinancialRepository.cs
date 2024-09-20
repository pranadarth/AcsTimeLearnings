
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
    public class SalesFinancialRepository : ISalesFinancialRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<SalesFinancialRepository> _logger;

        public SalesFinancialRepository(ILogger<SalesFinancialRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public object GetSalesSummaryDetails(string accountId, string deliveryAccountId, DateOnly orderedDate)
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "[sp_RPT_cust_order_sales_summary_Header]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@AccountId";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Value = accountId;
                cmd.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@DeliveryAccountId";
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Value = deliveryAccountId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@OrderDate";
                parameter2.SqlDbType = SqlDbType.Date;
                parameter2.Value = orderedDate;
                cmd.Parameters.Add(parameter2);

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

        public object GetSalesSummaryHeaderDetails(List<string> accountIds, List<string> months)
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "[sp_RPT_cust_order_sales_summary_Header]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@AccountId";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Value = string.Join(',', accountIds);
                cmd.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@Month";
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Value = string.Join(',', months);
                cmd.Parameters.Add(parameter1);

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
                string exception = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return null;
        }
    }
}
