using System;
using System.Collections.Generic;
using PactNetMessages.Mocks.MockMq.Models;


namespace PactNetMessages.Mocks.MockMq
{
    public class MockMq : IMockMq
    {
        private object contents;

        private string messageDescription;

        private readonly IMockMqRepository messageQueue;

        private object metaData;

        private string providerState;


        public MockMq()
        {
            messageQueue = new MockMqRepository();
        }

        public IMockMq Given(string state)
        {
            if (String.IsNullOrEmpty(state))
            {
                throw new ArgumentException("Please supply a non null or empty providerState");
            }

            providerState = state;

            return this;
        }

        public IMockMq UponReceiving(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Please supply a non null or empty description");
            }

            messageDescription = description;

            return this;
        }

        public IMockMq WithMetaData(object data)
        {
            metaData = data;
            return this;
        }

        public IMockMq WithContent(object data)
        {
            contents = data;
            RegisterInteraction();
            return this;
        }

        public void VerifyInteractions()
        {
        }

        public void ClearInteractions()
        {
        }

        private void RegisterInteraction()
        {
            if (String.IsNullOrEmpty(messageDescription))
            {
                throw new InvalidOperationException(
                    "description has not been set, please supply using the UponReceiving method.");
            }

            if (metaData == null)
            {
                throw new InvalidOperationException(
                    "meta data has not been set, please supply using the WithMetaData method.");
            }

            if (contents == null)
            {
                throw new InvalidOperationException(
                    "content has not been set, please supply using the WithContent method.");
            }

            var message = new Message
            {
                ProviderState = providerState,
                Description = messageDescription,
                Contents = contents,
                MetaData = metaData
            };

            messageQueue.AddInteraction(message);
            ClearTrasientState();
        }


        private void ClearTrasientState()
        {
            metaData = null;
            contents = null;
            providerState = null;
            messageDescription = null;
        }

        public IEnumerable<Message> Interactions()
        {
            return messageQueue.Interactions;
        }
    }
}