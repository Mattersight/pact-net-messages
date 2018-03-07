using System;
using System.Collections.Generic;
using System.Linq;
using PactNetMessages.Mocks.MockHttpService.Models;


namespace PactNetMessages.Mocks.MockMq
{
    internal class MockMqRepository : IMockMqRepository
    {
        private readonly List<Message> interactions = new List<Message>();

        ICollection<Message> IMockMqRepository.Interactions { get { return interactions; } }

        string IMockMqRepository.TestContext { get; set; }

        private readonly List<Message> testScopedInteractions = new List<Message>();

        ICollection<Message> IMockMqRepository.TestScopedInteractions { get { return testScopedInteractions; } }

        void IMockMqRepository.AddInteraction(Message interaction)
        {
            if (interaction == null)
            {
                throw new ArgumentNullException("interaction");
            }

            //You cannot have any duplicate interaction defined in a test scope
            if (testScopedInteractions.Any(x => x.Description == interaction.Description &&
                x.ProviderState == interaction.ProviderState))
            {
                throw new PactFailureException(String.Format(
                    "An interaction already exists with the description '{0}' and provider state '{1}' in this test. Please supply a different description or provider state.",
                    interaction.Description, interaction.ProviderState));
            }

            //From a Pact specification perspective, I should de-dupe any interactions that have been registered by another test as long as they match exactly!
            var duplicateInteractions = interactions.Where(x =>
                x.Description == interaction.Description && x.ProviderState == interaction.ProviderState).ToList();
            if (!duplicateInteractions.Any())
            {
                interactions.Add(interaction);
            }
            else if (duplicateInteractions.Any(di => di.AsJsonString() != interaction.AsJsonString()))
            {
                //If the interaction description and provider state match, however anything else in the interaction is different, throw
                throw new PactFailureException(String.Format(
                    "An interaction registered by another test already exists with the description '{0}' and provider state '{1}', however the interaction does not match exactly. Please supply a different description or provider state. Alternatively align this interaction to match the duplicate exactly.",
                    interaction.Description, interaction.ProviderState));
            }

            testScopedInteractions.Add(interaction);
        }

        public void ClearTestScopedState()
        {
            testScopedInteractions.Clear();
        }
    }
}