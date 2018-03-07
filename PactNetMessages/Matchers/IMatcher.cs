using Newtonsoft.Json.Linq;

namespace PactNetMessages.Matchers
{
    internal interface IMatcher
    {
        MatcherResult Match(string path, JToken expected, JToken actual);
    }
}