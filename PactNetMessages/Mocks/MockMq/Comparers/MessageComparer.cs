using PactNetMessages.Comparers;
using PactNetMessages.Mocks.MockMq.Models;


namespace PactNetMessages.Mocks.MockMq.Comparers
{
    public class MessageComparer : IComparer<Message>
    {
        private readonly IContentsComparer contentsComparer;

        private readonly IMetaDataComparer metaDataComparer;

        public MessageComparer()
        {
            contentsComparer = new ContentsComparer();
            metaDataComparer = new MetaDataComparer();
        }

        public ComparisonResult Compare(Message expected, Message actual)
        {
            var result = new ComparisonResult("returns a message which");

            if (expected == null)
            {
                result.RecordFailure(new ErrorMessageComparisonFailure("Expected message cannot be null"));
                return result;
            }

            if (expected.MetaData != null)
            {
                var metaDataComparerResult = metaDataComparer.Compare(expected.MetaData, actual.MetaData);
                result.AddChildResult(metaDataComparerResult);
            }

            if (expected.Contents != null)
            {
                var contentComparerResult = contentsComparer.Compare(expected.Contents, actual.Contents, expected.MatchingRules);
                result.AddChildResult(contentComparerResult);
            }

            return result;
        }
    }
}