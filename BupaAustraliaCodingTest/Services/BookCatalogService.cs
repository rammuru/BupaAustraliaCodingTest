using System.Net.Http.Json;
using BupaAustraliaCodingTest.Models;
using BupaAustraliaCodingTest.Options;
using Microsoft.Extensions.Options;
namespace BupaAustraliaCodingTest.Services
{
    public sealed class BookCatalogService : IBookCatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly BookApiOptions _options;

        public BookCatalogService(HttpClient httpClient, IOptions<BookApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<BookCatalogResult> GetBooksAsync(bool hardcoverOnly, CancellationToken cancellationToken)
        {
            var endpoint = string.IsNullOrWhiteSpace(_options.OwnersEndpoint)
                ? "/api/v1/bookowners"
                : _options.OwnersEndpoint;

            var owners = await _httpClient.GetFromJsonAsync<List<OwnerDto>>(endpoint, cancellationToken) ?? [];

            var allBooks = owners
                .SelectMany(owner => owner.Books.Select(book => new
                {
                    OwnerName = owner.Name,
                    OwnerAge = owner.Age,
                    BookName = book.Name,
                    BookType = book.Type
                }))
                .Where(x => !string.IsNullOrWhiteSpace(x.BookName));

            if (hardcoverOnly)
            {
                allBooks = allBooks.Where(x =>
                    string.Equals(x.BookType, "Hardcover", StringComparison.OrdinalIgnoreCase));
            }

            var adults = allBooks
                .Where(x => x.OwnerAge >= 18)
                .Select(x => new DisplayBookViewModel
                {
                    Name = x.BookName,
                    Type = x.BookType,
                    OwnerName = x.OwnerName
                })
                .OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var children = allBooks
                .Where(x => x.OwnerAge <= 17)
                .Select(x => new DisplayBookViewModel
                {
                    Name = x.BookName,
                    Type = x.BookType,
                    OwnerName = x.OwnerName
                })
                .OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return new BookCatalogResult
            {
                AdultBooks = adults,
                ChildrenBooks = children
            };
        }
    }
}
