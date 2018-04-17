using System;
using System.IO;
using PactNetMessages.Reporters.Outputters;
using Xunit;


namespace PactNetMessages.Tests
{
    public class PactProviderTest
    {
        public string Provider = "provider";
        public string Consumer = "consumer";
        
        public string PactFile;
        public IPactVerifier PactVerifier;
        public CustomOutputter Outputter;
        public PactVerifierConfig PactConfig;


        [Fact]
        public void PactProviderVerifyTest()
        {
            //set a output folder for logs and pacts retreived
            PactConfig = new PactVerifierConfig()
            {
                LogDir = "../../../Log"
            };
            //set a string output to easily assert against
            Outputter = new CustomOutputter();
            this.PactConfig.ReportOutputters.Add(Outputter);

            PactVerifier = new PactVerifier(() => { }, () => { }, this.PactConfig);
            PactFile = Path.Combine("../../../Pacts", $"{Consumer}-{Provider}.json".ToLower());

            //verify the interaction
                PactVerifier
                    .ProviderState("Testing Guid", setUp: GuidSetupState);

                //Verify will throw if there was a failure
                PactVerifier
                    .MessageProvider(Provider)
                    .HonoursPactWith(Consumer)
                    .PactUri(PactFile)
                    .Verify();

                Assert.Contains($"Verifying a Pact between {Consumer} and {Provider}", Outputter.Output);
        }

        /// <summary>
        /// Sets up the state required for the ProviderState
        /// </summary>
        /// <returns></returns>
        private PactNetMessages.Mocks.MockMq.Models.Message GuidSetupState()
        {
            var message = new { Id = new Guid() };
            var pactNetMessage = new PactNetMessages.Mocks.MockMq.Models.Message()
            {
                MetaData = new { },
                Contents = message
            };

            return pactNetMessage;
        }

        public class CustomOutputter : IReportOutputter
        {
            public string Output { get; private set; }

            public void Write(string report)
            {
                Output += report;
            }
        }
    }
}
