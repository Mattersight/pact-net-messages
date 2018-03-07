using PactNetMessages.Mocks.MockMq.Models;
using PactNetMessages.Validators;


namespace PactNetMessages.Mocks.MockMq.Validators
{
    internal interface IMessageProviderValidator : IPactValidator<MessagePactFile>
    {
    }
}