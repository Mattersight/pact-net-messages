using System;


namespace PactNetMessages.Infrastructure.Logging
{
    internal interface ILocalLogMessageHandler : IDisposable
    {
        string LogPath { get; }

        void Handle(LocalLogMessage logMessage);
    }
}