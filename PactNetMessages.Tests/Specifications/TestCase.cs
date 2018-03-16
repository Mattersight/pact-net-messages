using Newtonsoft.Json;
using PactNetMessages.Mocks.MockMq.Models;


namespace PactNetMessages.Tests.Specifications
{
    class TestCase
    {
        [JsonProperty(PropertyName = "actual")]
        public Message Actual { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "expected")]
        public Message Expected { get; set; }

        [JsonProperty(PropertyName = "match")]
        public bool Match { get; set; }
    }
}