using Newtonsoft.Json;


namespace PactNetMessages.Models
{
    public class PactFile : PactDetails
    {
        [JsonProperty(PropertyName = "metadata")]
        public dynamic Metadata { get; private set; }

        public PactFile()
        {
            Metadata = new
            {
                pactSpecificationVersion = "3.0"
            };
        }
    }
}