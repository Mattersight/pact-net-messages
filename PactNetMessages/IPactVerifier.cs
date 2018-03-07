using System;
using PactNetMessages.Mocks.MockHttpService.Models;


namespace PactNetMessages
{
    public interface IPactVerifier
    {
        /// <summary>
        /// Define a set up and/or tear down action for a specific state specified by the consumer.
        /// This is where you should set up test data, so that you can fulfil the contract outlined by a consumer.
        /// </summary>
        /// <param name="providerState">The name of the provider state as defined by the consumer interaction, which lives in the Pact file.</param>
        /// <param name="setUp">A set up function that will be run before the interaction verify.  The setup function must return a Message object for verification.</param>
        /// <param name="tearDown">A tear down action that will be run after the interaction verify, if the provider has specified it in the interaction. If no action is required please use an empty lambda () => {}.</param>
        IPactVerifier ProviderState(string providerState, Func<Message> setUp = null, Action tearDown = null);

        IPactVerifier MessageProvider(string providerName);

        IPactVerifier HonoursPactWith(string consumerName);

        IPactVerifier PactUri(string uri);

        void Verify(string description = null, string providerState = null);
    }
}