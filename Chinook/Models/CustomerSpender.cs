using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Models
{
    internal readonly record struct CustomerSpender(Customer Customer, double TotalSpent)
    {
        public override string? ToString()
        {
            return $"{Customer.CustomerId} {Customer.FirstName} {Customer.LastName}. Total spent: {TotalSpent}";
        }
    }
}
