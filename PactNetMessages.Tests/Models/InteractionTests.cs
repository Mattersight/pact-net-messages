using PactNetMessages.Models;
using Xunit;


namespace PactNetMessages.Tests.Models
{
    public class InteractionTests
    {
        [Fact]
        public void AsJsonString_WhenCalled_ReturnsJsonRepresentation()
        {
            const string expectedInteractionJson =
                "{\"description\":\"My description\",\"providerState\":\"My provider state\"}";

            var interaction = new Interaction
            {
                Description = "My description",
                ProviderState = "My provider state"
            };

            var actualInteractionJson = interaction.AsJsonString();

            Assert.Equal(expectedInteractionJson, actualInteractionJson);
        }
    }
}