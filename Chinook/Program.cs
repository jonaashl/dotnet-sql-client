using Microsoft.Data.SqlClient;

namespace Chinook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "N-NO-01-01-1451\\SQLEXPRESS";
            builder.IntegratedSecurity= true;
            builder.InitialCatalog = "Chinook";
            builder.TrustServerCertificate= true;
            Console.WriteLine(builder.ConnectionString);
        }
    }
}