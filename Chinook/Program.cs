using Chinook.Models;
using Chinook.Repositories;
using Chinook.Repositories.Customers;
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


            customerRepository.GetAll().ToList().ForEach(customer => Console.WriteLine(customer));
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