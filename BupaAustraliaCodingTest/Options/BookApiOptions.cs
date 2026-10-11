namespace BupaAustraliaCodingTest.Options
{
    public sealed class BookApiOptions
    {
        public const string SectionName = "BookApi";

        public string BaseUrl { get; set; } = string.Empty;

        public string OwnersEndpoint { get; set; } = "/api/v1/bookowners";
    }
}
