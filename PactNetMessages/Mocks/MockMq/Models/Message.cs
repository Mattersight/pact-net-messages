using System.Collections.Generic;
using Newtonsoft.Json;
using PactNetMessages.Matchers;
using PactNetMessages.Mocks.MockMq.Matchers;
using PactNetMessages.Models;


namespace PactNetMessages.Mocks.MockHttpService.Models
{
    public class Message : Interaction
    {
        [JsonProperty(PropertyName = "contents")]
        public dynamic Contents { get; set; }

        [JsonProperty(PropertyName = "metaData")]
        public dynamic MetaData { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "matchingRules")]
        internal IDictionary<string, IMatcher> MatchingRules { get; private set; }

        public Message()
        {
            MatchingRules = new Dictionary<string, IMatcher>
            {
                { ContentMatcher.DEFAULT_PATH, new ContentMatcher() }
            };
        }
    }
}