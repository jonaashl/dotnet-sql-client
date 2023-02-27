namespace Chinook.Models
{
    internal readonly record struct Customer(int CustomerId, string FirstName, string LastName, string? Country, string? PostalCode, string? Phone, string Email)
    {
        public override string? ToString()
        {
            return $"{CustomerId}\n{FirstName} {LastName}\n{PostalCode} {Country}\n{Phone}\n{Email}";
        }
    }
}
