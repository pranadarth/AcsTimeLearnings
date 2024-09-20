
using Athena.Application.RepositoryInterface.Reports;
using Athena.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository.Reports
{
    public class MealByWardRepository : IMealsByWardRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<MealByWardRepository> _logger;

        public MealByWardRepository(ILogger<MealByWardRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public object GetHospitalsList()
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "[sp_RPT_hospital_filter]";
                cmd.CommandType = CommandType.StoredProcedure;
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

        public object GetMealsList()
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "[sp_RPT_meal_filter]";
                cmd.CommandType = CommandType.StoredProcedure;
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

        public object GetPatientMeals(DateOnly fromDate, DateOnly toDate, string meal, string siteId, string wardId)
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "[sp_RPT_patient_meals_report_test]";
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

                if (!string.IsNullOrWhiteSpace(meal))
                {
                    SqlParameter accountNoParameter = new SqlParameter();
                    accountNoParameter.ParameterName = "@meal";
                    accountNoParameter.SqlDbType = SqlDbType.VarChar;
                    accountNoParameter.Value = meal;
                    cmd.Parameters.Add(accountNoParameter);
                }

                if (!string.IsNullOrWhiteSpace(siteId))
                {
                    SqlParameter orderNumberParameter = new SqlParameter();
                    orderNumberParameter.ParameterName = "@siteId";
                    orderNumberParameter.SqlDbType = SqlDbType.VarChar;
                    orderNumberParameter.Value = siteId;
                    cmd.Parameters.Add(orderNumberParameter);
                }

                if (!string.IsNullOrWhiteSpace(wardId))
                {
                    SqlParameter categoryMainParameter = new SqlParameter();
                    categoryMainParameter.ParameterName = "@wardId";
                    categoryMainParameter.SqlDbType = SqlDbType.VarChar;
                    categoryMainParameter.Value = wardId;
                    cmd.Parameters.Add(categoryMainParameter);
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

        public object GetWardsList(string siteId)
        {
            var con = _athenaDbcontext.Database.GetDbConnection();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "[sp_RPT_Ward_filter]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter categoryMainParameter = new SqlParameter();
                categoryMainParameter.ParameterName = "@SiteId";
                categoryMainParameter.SqlDbType = SqlDbType.VarChar;
                categoryMainParameter.Value = siteId;
                cmd.Parameters.Add(categoryMainParameter);

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
