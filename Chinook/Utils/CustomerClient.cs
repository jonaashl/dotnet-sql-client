using Chinook.Models;
using Chinook.Repositories.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Utils
{
    internal class CustomerClient
    {
        private ICustomerRepository _customerRepository;

        public CustomerClient(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void DoDataAccess()
        {
            _customerRepository.GetAll().ForEach(customer => Console.WriteLine(customer));
            
            Customer Jonas = new Customer() {FirstName = "Jonas", LastName = "Lindevall", Country = "Norway", PostalCode = "1281", Phone = "99999", Email = "bigsecret@gmail.com" };
            _customerRepository.Add(Jonas);

            _customerRepository.GetById(10);

        }
    }
}
