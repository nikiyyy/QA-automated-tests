using NUnit.Framework;

namespace Github_API_Tests
{
    public class IssueResponse
    {
        public long id { get; set; }
        public long number { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}