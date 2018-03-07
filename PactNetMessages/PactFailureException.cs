using System;


namespace PactNetMessages
{
    public class PactFailureException : Exception
    {
        public PactFailureException(string message)
            : base(message)
        {
        }
    }
}