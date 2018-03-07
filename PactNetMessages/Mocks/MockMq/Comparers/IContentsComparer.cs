using System.Collections.Generic;
using PactNetMessages.Comparers;
using PactNetMessages.Matchers;


namespace PactNetMessages.Mocks.MockMq.Comparers
{
    interface IContentsComparer
    {
        ComparisonResult Compare(dynamic expected, dynamic actual, IDictionary<string, IMatcher> matchingRules);
    }
}