using PactNetMessages.Comparers;


namespace PactNetMessages.Mocks.MockMq.Comparers
{
    internal interface IMetaDataComparer
    {
        ComparisonResult Compare(dynamic expected, dynamic actual);
    }
}