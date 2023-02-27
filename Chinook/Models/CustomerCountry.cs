namespace Chinook.Models
{
    internal readonly record struct CustomerCountry(string Country, int Count)
    {
        public override string? ToString()
        {
            return $"{Country}: {Count}";
        }
    }
}
