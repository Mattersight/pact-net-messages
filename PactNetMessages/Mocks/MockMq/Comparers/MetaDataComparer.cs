﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PactNetMessages.Comparers;


namespace PactNetMessages.Mocks.MockMq.Comparers
{
    class MetaDataComparer : IMetaDataComparer
    {
        public ComparisonResult Compare(dynamic expected, dynamic actual)
        {
            var result = new ComparisonResult("includes metadata");

            if (expected == null)
            {
                return result;
            }

            if (expected != null && actual == null)
            {
                result.RecordFailure(new ErrorMessageComparisonFailure("Actual metadata is null"));
                return result;
            }

            var expectedToken = JToken.FromObject(expected);
            
            //serializing first to match the same process as loading the pact from file
            var actualToken = JToken.Parse(JsonConvert.SerializeObject(actual));

            if (!JToken.DeepEquals(expectedToken, actualToken))
            {
                result.RecordFailure(new DiffComparisonFailure(expected, actual));
            }
            else
            {
                result.AddChildResult(new ComparisonResult(JsonConvert.SerializeObject(actualToken)));
            }

            return result;
        }
    }
}