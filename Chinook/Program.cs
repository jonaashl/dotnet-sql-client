using Chinook.Models;
using Chinook.Repositories;
using Chinook.Repositories.Customers;
using Chinook.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;

namespace Chinook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICustomerRepository customerRepository = new CustomerRepository(GetConnectionString());
            CustomerClient customerClient = new CustomerClient(customerRepository);
            customerClient.DoDataAccess();
        }
        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new();
            // builder.DataSource = "N-NO-01-01-1451\\SQLEXPRESS";  // Jonas
            builder.DataSource = "STEFFENS-DESKTO\\SQLEXPRESS01";   // Steffen
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Chinook";
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }
    }
}