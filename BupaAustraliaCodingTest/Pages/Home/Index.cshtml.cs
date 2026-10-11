using BupaAustraliaCodingTest.Models;
using BupaAustraliaCodingTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BupaAustraliaCodingTest.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookCatalogService _bookCatalogService;

        public IndexModel(ILogger<IndexModel> logger, IBookCatalogService bookCatalogService)
        {
            _logger = logger;
            _bookCatalogService = bookCatalogService;
        }
        [BindProperty(SupportsGet = true)]
        public bool GetBooks { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool HardcoverOnly { get; set; }

        public IReadOnlyList<DisplayBookViewModel> AdultBooks { get; private set; } = [];

        public IReadOnlyList<DisplayBookViewModel> ChildrenBooks { get; private set; } = [];

        public string? ErrorMessage { get; private set; }


        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            if (!GetBooks)
            {
                return;
            }

            try
            {
                var result = await _bookCatalogService.GetBooksAsync(HardcoverOnly, cancellationToken);
                AdultBooks = result.AdultBooks;
                ChildrenBooks = result.ChildrenBooks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load books from API.");
                ErrorMessage = "Unable to load data from the API. Please verify API settings and try again.";
            }
        }
    }
}
