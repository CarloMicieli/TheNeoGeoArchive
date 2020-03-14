namespace TheNeoGeoArchive.IntegrationTests.Responses
{
    public sealed class ErrorResponse
    {
        public string Type { set; get; }
        public string Title { set; get; }
        public int Status { set; get; }
        public string Detail { set; get; }
        public string TraceId { set; get; }
    }
}
