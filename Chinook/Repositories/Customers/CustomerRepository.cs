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

        public int Delete(int obj)
        {
            throw new NotImplementedException();
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
            if (reader.NextResult())
            {
                customer = new Customer()
                {
                    CustomerId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Country = reader.GetString(3),
                    PostalCode = reader.GetString(4),
                    Phone = reader.GetString(5),
                    Email = reader.GetString(6)
                };
            } else
            {
                throw new Exception("This doesnt work");
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
