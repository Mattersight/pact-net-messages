using System;
using System.IO;
using Newtonsoft.Json;
using PactNetMessages.Configuration.Json;
using PactNetMessages.Mocks.MockMq;
using PactNetMessages.Mocks.MockMq.Models;
using PactNetMessages.Models;


namespace PactNetMessages
{
    public class PactMessageBuilder : IPactMessageBuilder
    {
        private readonly IMockMq mockMq;

        private readonly PactConfig pactConfig;

        public string ConsumerName { get; private set; }

        public string ProviderName { get; private set; }

        public PactMessageBuilder()
        {
            pactConfig = new PactConfig();
            mockMq = new MockMq();
        }

        public IPactMessageBuilder ServiceConsumer(string consumerName)
        {
            if (String.IsNullOrEmpty(consumerName))
            {
                throw new ArgumentException("Please supply a non null or empty consumerName");
            }

            ConsumerName = consumerName;

            return this;
        }

        public IPactMessageBuilder HasPactWith(string providerName)
        {
            if (String.IsNullOrEmpty(providerName))
            {
                throw new ArgumentException("Please supply a non null or empty providerName");
            }

            ProviderName = providerName;

            return this;
        }


        public void Build()
        {
            if (mockMq == null)
            {
                throw new InvalidOperationException(
                    "The Pact file could not be saved because the mock message queue is not initialised. Please initialise by calling the MockMq() method.");
            }

            PersistPactFile();
        }

        private void PersistPactFile()
        {
            if (String.IsNullOrEmpty(ConsumerName))
            {
                throw new InvalidOperationException(
                    "ConsumerName has not been set, please supply a consumer name using the ServiceConsumer method.");
            }

            if (String.IsNullOrEmpty(ProviderName))
            {
                throw new InvalidOperationException(
                    "ProviderName has not been set, please supply a provider name using the HasPactWith method.");
            }

            var pactDetails = new PactDetails
            {
                Provider = new Pacticipant {Name = ProviderName},
                Consumer = new Pacticipant {Name = ConsumerName}
            };

            var pactFilePath = Path.Combine(pactConfig.PactDir, pactDetails.GeneratePactFileName());

            var pactFile = new MessagePactFile
            {
                Provider = pactDetails.Provider,
                Consumer = pactDetails.Consumer,
                Messages = mockMq.Interactions()
            };

            var pactFileJson = JsonConvert.SerializeObject(pactFile, JsonConfig.PactFileSerializerSettings);

            try
            {
                File.WriteAllText(pactFilePath, pactFileJson);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(pactConfig.PactDir);
                File.WriteAllText(pactFilePath, pactFileJson);
            }
        }

        public IMockMq MockMq()
        {
            return mockMq;
        }
    }
}