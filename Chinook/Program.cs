using Chinook.Repositories.Customers;
using Chinook.Utils;
using Microsoft.Data.SqlClient;

namespace Chinook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // testing with mock data
            ICustomerRepository customerRepository = new CustomerRepository(GetConnectionString());
            CustomerClient customerClient = new(customerRepository);
            customerClient.DoTheStuff();
        }
        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "YOUR_CONNECTION_STRING_HERE",
                IntegratedSecurity = true,
                InitialCatalog = "Chinook",
                TrustServerCertificate = true
            };
            return builder.ConnectionString;
        }
    }
}