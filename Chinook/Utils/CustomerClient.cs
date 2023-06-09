﻿using Chinook.Models;
using Chinook.Repositories.Customers;

namespace Chinook.Utils
{
    internal class CustomerClient
    {
        private ICustomerRepository _customerRepository;

        public CustomerClient(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// For testing the repository methods
        /// </summary>
        public void DoTheStuff()
        {
            _customerRepository.GetAll().ForEach(customer => Console.WriteLine(customer));

            Customer Steffen = new() {FirstName = "Steffen", LastName = "Tommeras", Country = "Norway", PostalCode = "1274", Phone = "99991", Email = "biggersecret@gmail.com" };
            _customerRepository.Add(Steffen);

            Console.WriteLine("\nGetting ID:");
            Console.WriteLine(_customerRepository.GetById(10));
            Console.WriteLine("\nGetting Name:");
            Console.WriteLine(_customerRepository.GetCustomerByName("Steffe"));

            Console.WriteLine("\nUpdating user:");
            _customerRepository.Update(new Customer(1, "Jonas", "Thunderbolt", "Turkmenistan", "69420", "98989898", "notsosecret@hotmale.com"));
            Console.WriteLine(_customerRepository.GetCustomerByName("Jona"));

            Console.WriteLine("\nGetting with offset and limit:");
            _customerRepository.GetCustomerPage(10, 5).ForEach(c => Console.WriteLine(c));

            Console.WriteLine("\nGetting number of customers per country:");
            _customerRepository.GetCustomerCountPerCountry().ForEach(cc => Console.WriteLine(cc));

            Console.WriteLine("\nGetting customers in order of spending:");
            _customerRepository.GetHighestSpenders().ForEach(cs => Console.WriteLine(cs));

            Console.WriteLine("\nGetting most popular genre for a customer:");
            Console.WriteLine(_customerRepository.GetCustomerMostPopularGenre(_customerRepository.GetById(1))); // one genre
            Console.WriteLine(_customerRepository.GetCustomerMostPopularGenre(_customerRepository.GetById(12))); // several genres
        }
    }
}
