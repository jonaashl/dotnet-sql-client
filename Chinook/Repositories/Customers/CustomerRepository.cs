using Chinook.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories.Customers
{
    internal class CustomerRepository : ICustomerRepository
    {
        private string _connectionString;

        // Probably have to update this if we move connection string to separate class.
        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(Customer obj)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Customer(CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email) VALUES(@CustomerId, @FirstName, @LastName, @Country, @PostalCode, @Phone, @Email";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                    cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@Country", obj.Country);
                    cmd.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
                    cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);

                    ret = cmd.ExecuteNonQuery();
                }
            }
            return ret;
        }

        public int Delete(int obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                Console.WriteLine("Connected");
                string sql = "SELECT * FROM Customer";
                using (SqlCommand cmd = new(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            return customers;
        }

        public IEnumerable<Customer> GetById(int id)
        {
            throw new NotImplementedException();
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
