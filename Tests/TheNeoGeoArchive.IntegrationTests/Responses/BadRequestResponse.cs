using System.Collections.Generic;

namespace TheNeoGeoArchive.IntegrationTests.Responses
{
    public sealed class BadRequestResponse
    {
        public string Type { set; get; }
        public string Title { set; get; }
        public int Status { set; get; }
        public string TraceId { set; get; }
        public Dictionary<string, List<string>> Errors { set; get; }
    }
}
