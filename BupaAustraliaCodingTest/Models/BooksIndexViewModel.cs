namespace BupaAustraliaCodingTest.Models
{
    public sealed class BooksIndexViewModel
    {
        public bool GetBooks { get; set; }

        public bool HardcoverOnly { get; set; }

        public IReadOnlyList<DisplayBookViewModel> AdultBooks { get; set; } = [];

        public IReadOnlyList<DisplayBookViewModel> ChildrenBooks { get; set; } = [];

        public string? ErrorMessage { get; set; }
    }
}
