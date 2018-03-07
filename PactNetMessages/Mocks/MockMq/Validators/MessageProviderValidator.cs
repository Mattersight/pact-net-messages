using System;
using System.Linq;
using PactNetMessages.Comparers;
using PactNetMessages.Logging;
using PactNetMessages.Mocks.MockHttpService.Models;
using PactNetMessages.Mocks.MockMq.Comparers;
using PactNetMessages.Mocks.MockMq.Models;
using PactNetMessages.Models;
using PactNetMessages.Reporters;


namespace PactNetMessages.Mocks.MockMq.Validators
{
    internal class MessageProviderValidator
    {
        private readonly PactVerifierConfig _config;

        private readonly IReporter _reporter;

        public MessageProviderValidator(
            IReporter reporter,
            PactVerifierConfig config)
        {
            _reporter = reporter;
            _config = config;
        }

        public void Validate(MessagePactFile pactFile, ProviderStates providerStates)
        {
            if (pactFile == null)
            {
                throw new ArgumentException("Please supply a non null pactFile");
            }

            if (pactFile.Consumer == null || String.IsNullOrEmpty(pactFile.Consumer.Name))
            {
                throw new ArgumentException("Please supply a non null or empty Consumer name in the pactFile");
            }

            if (pactFile.Provider == null || String.IsNullOrEmpty(pactFile.Provider.Name))
            {
                throw new ArgumentException("Please supply a non null or empty Provider name in the pactFile");
            }

            if (pactFile.Messages != null && pactFile.Messages.Any())
            {
                _reporter.ReportInfo(String.Format("Verifying a Pact between {0} and {1}", pactFile.Consumer.Name,
                    pactFile.Provider.Name));

                var comparisonResult = new ComparisonResult();

                foreach (var interaction in pactFile.Messages)
                {
                    InvokePactSetUpIfApplicable(providerStates);

                    _reporter.ResetIndentation();

                    ProviderState providerStateItem = null;
                    Message actualMessage = null;

                    if (interaction.ProviderState != null)
                    {
                        try
                        {
                            providerStateItem = providerStates.Find(interaction.ProviderState);
                        }
                        catch (Exception)
                        {
                            providerStateItem = null;
                        }

                        //either not found in providerStates or exception was caught
                        if (providerStateItem == null)
                        {
                            throw new InvalidOperationException(String.Format(
                                "providerState '{0}' was defined by a consumer, however could not be found. Please supply this provider state.",
                                interaction.ProviderState));
                        }

                        try
                        {
                            actualMessage = InvokeProviderStateSetUp(providerStateItem);
                        }
                        catch (Exception e)
                        {
                            throw new InvalidOperationException(String.Format(
                                "Actual message could not be generated for providerState '{0}'. error: {1}",
                                interaction.ProviderState, e.Message));
                        }
                    }


                    if (!String.IsNullOrEmpty(interaction.ProviderState))
                    {
                        _reporter.Indent();
                        _reporter.ReportInfo(String.Format("Given {0}", interaction.ProviderState));
                    }

                    _reporter.Indent();
                    _reporter.ReportInfo(String.Format("Upon receiving {0}", interaction.Description));


                    try
                    {
                        var interactionComparisonResult = ValidateInteraction(interaction, actualMessage);
                        comparisonResult.AddChildResult(interactionComparisonResult);
                        _reporter.Indent();
                        _reporter.ReportSummary(interactionComparisonResult);
                    }
                    finally
                    {
                        InvokeProviderStateTearDownIfApplicable(providerStateItem);
                        InvokeTearDownIfApplicable(providerStates);
                    }
                }

                _reporter.ResetIndentation();
                _reporter.ReportFailureReasons(comparisonResult);
                _reporter.Flush();

                if (comparisonResult.HasFailure)
                {
                    throw new PactFailureException(String.Format("See test output or {0} for failure details.",
                        !String.IsNullOrEmpty(_config.LoggerName)
                            ? LogProvider.CurrentLogProvider.ResolveLogPath(_config.LoggerName) : "logs"));
                }
            }
        }

        private ComparisonResult ValidateInteraction(Message interaction, Message actualMessage)
        {
            var comparer = new MessageComparer();
            return comparer.Compare(interaction, actualMessage);
        }

        private void InvokePactSetUpIfApplicable(ProviderStates providerStates)
        {
            if (providerStates != null && providerStates.SetUp != null)
            {
                providerStates.SetUp();
            }
        }

        private void InvokeTearDownIfApplicable(ProviderStates providerStates)
        {
            if (providerStates != null && providerStates.TearDown != null)
            {
                providerStates.TearDown();
            }
        }

        private Message InvokeProviderStateSetUp(ProviderState providerState)
        {
            if (providerState.SetUp == null)
            {
                throw new Exception("Expected provider state set up defined.");
            }

            return providerState.SetUp();
        }

        private void InvokeProviderStateTearDownIfApplicable(ProviderState providerState)
        {
            if (providerState != null && providerState.TearDown != null)
            {
                providerState.TearDown();
            }
        }
    }
}