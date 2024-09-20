
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Database.Utils
{
    public  class SqlHelper
    {
        public async Task<DataTable> ExecuteSql(AthenaDbContext dbContext, string sql, IEnumerable<DbParameter> parameters = null)
        {
            var con = dbContext.Database.GetDbConnection();
            await con.OpenAsync();

            var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(parameters?.ToArray() ?? new DbParameter[0]);

            var dataReader = await cmd.ExecuteReaderAsync();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);

            await con.CloseAsync();

            return dataTable;
        }
    }
}
