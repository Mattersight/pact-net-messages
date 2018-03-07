using PactNetMessages.Mocks.MockMq;


namespace PactNetMessages
{
    public interface IPactMessageBuilder
    {
        IPactMessageBuilder ServiceConsumer(string consumerName);

        IPactMessageBuilder HasPactWith(string providerName);

        IMockMq MockMq();

        void Build();
    }
}