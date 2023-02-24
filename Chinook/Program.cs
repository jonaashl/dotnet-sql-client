using Chinook.Models;
using Chinook.Repositories.Customers;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Chinook
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
        }

        private static string GetConnectionString()
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