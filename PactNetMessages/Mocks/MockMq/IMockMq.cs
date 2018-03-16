using System.Collections.Generic;
using PactNetMessages.Mocks.MockMq.Models;


namespace PactNetMessages.Mocks.MockMq
{
    public interface IMockMq : IMockProvider<IMockMq>
    {
        IMockMq WithMetaData(object metaData);

        IMockMq WithContent(object content);

        void ClearInteractions();

        void VerifyInteractions();

        IEnumerable<Message> Interactions();
    }
}