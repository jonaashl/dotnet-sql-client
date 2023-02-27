using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Models
{
    internal readonly record struct CustomerGenre(Customer Customer, List<string> Genres)
    {
        public override string? ToString()
        {
            if (Genres.Count == 1) return $"{Customer.CustomerId} {Customer.FirstName} {Customer.LastName}. Favorite genre: {Genres[0]}";

            return $"{Customer.CustomerId} {Customer.FirstName} {Customer.LastName}. Favorite genres: {String.Join(", ", Genres)}";
        }
    }
}
