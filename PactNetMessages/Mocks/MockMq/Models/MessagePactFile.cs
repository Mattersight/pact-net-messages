using System.Collections.Generic;
using Newtonsoft.Json;
using PactNetMessages.Mocks.MockHttpService.Models;
using PactNetMessages.Models;


namespace PactNetMessages.Mocks.MockMq.Models
{
    public class MessagePactFile : PactFile
    {
        [JsonProperty(PropertyName = "messages")]
        public IEnumerable<Message> Messages { get; set; }
    }
}