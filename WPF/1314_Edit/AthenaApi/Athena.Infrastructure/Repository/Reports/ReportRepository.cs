
using Athena.Application.RepositoryInterface.Reports;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Athena.Infrastructure.Database.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Athena.Infrastructure.Repository.Reports
{
    public class ReportRepository : IReportRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<ReportRepository> _logger;

        public ReportRepository(ILogger<ReportRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public object GetReportDetails(string reportId, string clientId)
        {
            if (reportId == "1")
            {

                object result = GetDetailsFromView("vw_sales_summary_header");
                return null;
            }

            ReportDetails reportDetails = new ReportDetails();
            List<Component> components = new List<Component>();

            //Table Component 
            Component tableComponent = new Component()
            {
                Name = "Table",
                Type = "TABLE",
                Header = new List<string> { "Supplier", "Missing", "Provided", "Total" },
                Rows = new List<TableRow> { new TableRow{ TableData = new List<TableDataDefinition>{ new TableDataDefinition{ Value="Bitfood"},
                                                                              new TableDataDefinition{ Value="1",DrillDown= new {Params=new{SupplierId="123",DeclarationStatus="Missing" } } },
                                                                              new TableDataDefinition{ Value="26",DrillDown= new {Params=new{SupplierId="123",DeclarationStatus="Provided" } } },
                                                                              new TableDataDefinition{ Formula=new Formula{Name= "Add", Columns = new List<string> { "Missing", "Provided" } } }}},
                                        }

            };

            for (int i = 0; i < 14; i++)
            {

                tableComponent.Rows.Add(new TableRow
                {
                    TableData = new List<TableDataDefinition>{ new TableDataDefinition{ Value="Clayton "+i},
                                                                              new TableDataDefinition{ Value="2",DrillDown= new {Params=new{SupplierId="222",DeclarationStatus="Missing" } } },
                                                                              new TableDataDefinition{ Value="27",DrillDown= new {Params=new{SupplierId="222",DeclarationStatus="Provided" } } },
                                                                              new TableDataDefinition{ Formula=new Formula{Name= "Add", Columns = new List<string> { "Missing", "Provided" } } }}
                });
            }

            components.Add(tableComponent);


            //Bar chart
            Component barChartComponent = new Component()
            {
                Name = "BarChart",
                Type = "CHART",
                Labels = new List<string> { "Bitfood", "Apetito", "Ewoods" },
                IsVertical = true,
                Datasets = new List<Dataset> { new Dataset { Label = "Missed", Data = new List<int> { 10, 20, 30 }, BackgroundColor = "#1BD6E9",BorderColor = "#1BD6E9" },
                    new Dataset { Label = "Provided", Data = new List<int> { 20, 30, 40 }, BackgroundColor = "#1BD6E9", BorderColor = "#1BD6E9" } }
            };

            components.Add(barChartComponent);
            reportDetails.Components = components;

            return reportDetails;
        }

        public async Task<DataTable> GetDetailsFromView(string viewName, IEnumerable<DbParameter> parameters = null)
        {
            try
            {
                string commandText = "SELECT * FROM [dbo].[" + viewName + "]";

                SqlHelper sqlHelper = new SqlHelper();
                return sqlHelper.ExecuteSql(_athenaDbcontext, commandText, parameters).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<DataTable> GetDetailsFromSp(string spName, IEnumerable<DbParameter> parameters = null)
        {
            try
            {
                string commandText = "[" + spName + "]";

                var cmd = _athenaDbcontext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlHelper sqlHelper = new SqlHelper();
                return sqlHelper.ExecuteSql(_athenaDbcontext, commandText, parameters).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
