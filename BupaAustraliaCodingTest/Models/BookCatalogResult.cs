namespace BupaAustraliaCodingTest.Models
{
    public sealed class BookCatalogResult
    {
        public IReadOnlyList<DisplayBookViewModel> AdultBooks { get; init; } = [];

        public IReadOnlyList<DisplayBookViewModel> ChildrenBooks { get; init; } = [];
    }
}
