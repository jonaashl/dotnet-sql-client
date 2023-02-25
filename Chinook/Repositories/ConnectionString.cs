using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories
{
    public class ConnectionString
    {
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = "N-NO-01-01-1451\\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Chinook";
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }
    }
}
