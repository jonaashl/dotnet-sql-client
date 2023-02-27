using Chinook.Models;
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

        /// <summary>
        /// Gets all the customers in the database.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Finds the user with id equal to <c>id</c> in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Finds the user with name equal to <c>name</c> in the database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Gets a page with customers from the database with classic <c>limit</c> and <c>offset</c> params.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<Customer> GetCustomerPage(int limit, int offset)    // could (should?) be just one method for GetAll and this, with limit and offset defaulting to 0.
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

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Updates a user in the database.
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Gets the number of customers per country in the database.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all customers and their spendings, sorted by most spent.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a customers most popular genre, which in this context is the genre that corresponds to the most tracks from invoices associated to that customer.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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