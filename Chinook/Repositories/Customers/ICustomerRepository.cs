﻿using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories.Customers
{
    internal interface ICustomerRepository : ICrudRepository<Customer, int>
    {
        Customer GetCustomerByName(string name);
        List<Customer> GetCustomerPage(int limit, int offset);
        List<CustomerCountry> GetCustomerCountPerCountry();
        List<Customer> GetHighestSpenders();
        List<string> GetCustomerMostPopularGenre(Customer obj);
    }
}
