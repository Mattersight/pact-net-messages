using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PactNetMessages.Comparers;
using PactNetMessages.Matchers;
using PactNetMessages.Mocks.MockMq.Matchers;


namespace PactNetMessages.Mocks.MockMq.Comparers
{
    class ContentsComparer : IContentsComparer
    {

        public ComparisonResult Compare(dynamic expected, dynamic actual, IDictionary<string, IMatcher> matchingRules)
        {
            var result = new ComparisonResult("has matching content");

            if (expected == null)
            {
                return result;
            }

            if (expected != null && actual == null)
            {
                result.RecordFailure(new ErrorMessageComparisonFailure("Actual Content is null"));
                return result;
            }

            var expectedTokens = JToken.FromObject(expected);

            //serializing first to match the same process as loading the pact from file
            var actualTokens = JToken.Parse(JsonConvert.SerializeObject(actual));


            foreach (var rule in matchingRules)
            {
                MatcherResult matchResult = rule.Value.Match(rule.Key, expectedTokens, actualTokens);

                foreach (var check in matchResult.MatcherChecks)
                {
                    if (check is FailedMatcherCheck)
                    {
                        result.RecordFailure(new ErrorMessageComparisonFailure($"Path {check.Path}, expected: {expectedTokens.SelectToken(check.Path)}, actual: {actualTokens.SelectToken(check.Path)}"));
                    }
                }
            }
            

            return result;
        }
        
    }
}