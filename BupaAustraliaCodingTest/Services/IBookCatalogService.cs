using BupaAustraliaCodingTest.Models;
namespace BupaAustraliaCodingTest.Services
{
    public interface IBookCatalogService
    {
        Task<BookCatalogResult> GetBooksAsync(bool hardcoverOnly, CancellationToken cancellationToken);
    }
}
