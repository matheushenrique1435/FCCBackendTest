using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCBackendTest.Backend.DbContext
{
    public class RelationalDatabaseContext
    {
        public IDbConnection DatabaseConnection { get; private set; }
        public RelationalDatabaseContext(string connectionString) 
        {
            DatabaseConnection = new SqlConnection(connectionString);
        }
    }
}
