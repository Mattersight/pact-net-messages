using System;
using PactNetMessages.Mocks.MockMq.Models;


namespace PactNetMessages.Models
{
    public class ProviderState
    {
        public string ProviderStateDescription { get; }

        public Func<Message> SetUp { get; }

        public Action TearDown { get; }


        public ProviderState(string providerState, Func<Message> setUp, Action tearDown = null)
        {
            ProviderStateDescription = providerState;
            SetUp = setUp;
            TearDown = tearDown;
        }
    }
}