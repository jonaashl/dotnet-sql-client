using Chinook.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories.Customers
{
    internal class CustomerRepository : ICustomerRepository
    {
        private string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new();
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            Console.WriteLine("Connected");
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";
            using (SqlCommand cmd = new(sql, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    customers.Add(new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.IsDBNull(3) ? null : reader.GetString(3),
                        reader.IsDBNull(4) ? null : reader.GetString(4),
                        reader.IsDBNull(5) ? null : reader.GetString(5),
                        reader.GetString(6))
                    );
                }
            return customers;
        }

        public Customer GetById(int id)
        {
            Customer customer;
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @CustomerId";

            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("Gotten by id");
            if (reader.Read())
            {
                customer = new Customer(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6)
                );
                Console.WriteLine(customer.FirstName);
            }
            else
            {
                throw new Exception("There is no customer with that ID.");
            }
            return customer;
        }

        public Customer GetCustomerByName(string name)
        {
            Customer customer;
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE FirstName LIKE @Name OR LastName LIKE @Name";

            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
            using SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("Gotten by name:");
            if (reader.Read())
            {
                customer = new Customer(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6)
                );
                Console.WriteLine(customer.FirstName);
            }
            else
            {
                throw new Exception("There is no customer with that name.");
            }
            return customer;
        }

        public List<Customer> GetCustomerPage(int limit, int offset)
        {
            List<Customer> customers = new();
            using SqlConnection conn = new(_connectionString);
            conn.Open();

            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer ORDER BY CustomerId OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";

            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@Limit", limit);
            cmd.Parameters.AddWithValue("@Offset", offset);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new Customer(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.IsDBNull(3) ? null : reader.GetString(3),
                    reader.IsDBNull(4) ? null : reader.GetString(4),
                    reader.IsDBNull(5) ? null : reader.GetString(5),
                    reader.GetString(6))
                );
            }
            return customers;
        }

        public void Add(Customer obj)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email) VALUES(@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                    cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@Country", obj.Country);
                    cmd.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
                    cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.ExecuteNonQuery();
                };
            }
        }

        public void Update(Customer obj)
        {
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            string sql = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Country = @Country, PostalCode = @PostalCode, Phone = @Phone, Email = @Email WHERE CustomerId = @CustomerId";

            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@Country", obj.Country);
            cmd.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
            cmd.Parameters.AddWithValue("@Phone", obj.Phone);
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.ExecuteNonQuery();
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            Console.WriteLine("Connected");
            string sql = "SELECT * FROM Customer";
            using (SqlCommand cmd = new(sql, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            while (reader.Read())
                {
                    customers.Add(new Customer()
                    {
                        CustomerId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Country = reader.IsDBNull(7) ? null : reader.GetString(7),
                        PostalCode = reader.IsDBNull(8) ? null : reader.GetString(8),
                        Phone = reader.IsDBNull(9) ? null : reader.GetString(9),
                        Email = reader.GetString(11)
                    });
                }
            return customers;
        }

        public Customer GetById(int id)
        {
            Customer customer;
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @CustomerId";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("Gotten by id");
            if (reader.NextResult())
            {
                customer = new Customer(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6)
                );
            } else
            {
                throw new Exception("WIIIII");
            }
            return customer;
        }

        public IEnumerable<Customer> GetCustomerByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
