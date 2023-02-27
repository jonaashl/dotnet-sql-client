﻿using Chinook.Models;
using Microsoft.Data.SqlClient;

namespace Chinook.Repositories.Customers
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new();

            using SqlConnection conn = new(_connectionString);
            conn.Open();

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
            using SqlConnection conn = new(_connectionString);
            conn.Open();

            string sql = "INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email) VALUES(@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";

            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@Country", obj.Country);
            cmd.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
            cmd.Parameters.AddWithValue("@Phone", obj.Phone);
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.ExecuteNonQuery();
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

        public List<CustomerCountry> GetCustomerCountPerCountry()
        {
            List<CustomerCountry> customersPerCountry = new();

            using SqlConnection conn = new(_connectionString);
            conn.Open();

            string sql = "SELECT Country, COUNT(*) as CustomerCount FROM Customer GROUP BY Country ORDER BY CustomerCount DESC";

            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string country = (string)reader["Country"];
                int count = (int)reader["CustomerCount"];

                customersPerCountry.Add(new CustomerCountry(country, count));
            }

            return customersPerCountry;
        }

        public List<CustomerSpender> GetHighestSpenders()
        {
            List<CustomerSpender> customerSpenders = new();

            using SqlConnection conn = new(_connectionString);
            conn.Open();

            string sql = "SELECT c.CustomerId, c.FirstName, c.LastName, c.Country, c.PostalCode, c.Phone, c.Email, SUM(i.Total) AS TotalSpent FROM Customer c JOIN Invoice i ON c.CustomerId = i.CustomerId GROUP BY c.CustomerId, c.FirstName, c.LastName, c.Country, c.PostalCode, c.Phone, c.Email ORDER BY TotalSpent DESC";

            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customerSpenders.Add(new CustomerSpender(
                                new Customer(reader.GetInt32(0),
                                                reader.GetString(1),
                                                reader.GetString(2),
                                                reader.IsDBNull(3) ? null : reader.GetString(3),
                                                reader.IsDBNull(4) ? null : reader.GetString(4),
                                                reader.IsDBNull(5) ? null : reader.GetString(5),
                                                reader.GetString(6)),
                                (double)reader.GetDecimal(7))
                );
            }

            return customerSpenders;
        }

        public CustomerGenre GetCustomerMostPopularGenre(Customer obj)
        {
            List<string> genres = new();

            using SqlConnection conn = new(_connectionString);
            conn.Open();
            string sql = "SELECT TOP 1 WITH TIES g.Name, COUNT(*) AS Count FROM InvoiceLine il INNER JOIN Track t ON il.TrackId = t.TrackId INNER JOIN Genre g ON t.GenreId = g.GenreId INNER JOIN Invoice i ON il.InvoiceId = i.InvoiceId WHERE i.CustomerId = @CustomerId GROUP BY g.Name ORDER BY Count DESC";

            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                genres.Add(reader.GetString(0));
            }

            return new CustomerGenre(obj, genres);
        }
    }
}