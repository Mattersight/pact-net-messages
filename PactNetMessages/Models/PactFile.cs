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
                pactSpecification = new
                {
                    version = "3.0.0"
                },
                pactSpecificationVersion = "3.0"
            };
        }
    }
}