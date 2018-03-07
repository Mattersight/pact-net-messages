using Newtonsoft.Json;


namespace PactNetMessages.Models
{
    public class Pacticipant
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}