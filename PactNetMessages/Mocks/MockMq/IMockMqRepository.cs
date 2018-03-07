using System.Collections.Generic;
using PactNetMessages.Mocks.MockHttpService.Models;


namespace PactNetMessages.Mocks.MockMq
{
    internal interface IMockMqRepository
    {
        // all registered interactions
        ICollection<Message> Interactions { get; }

        string TestContext { get; set; }

        // all interactions attempted to register
        ICollection<Message> TestScopedInteractions { get; }

        void AddInteraction(Message interaction);

        void ClearTestScopedState();
    }
}