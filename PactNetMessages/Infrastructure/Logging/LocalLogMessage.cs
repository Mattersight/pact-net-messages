using System;
using PactNetMessages.Logging;


namespace PactNetMessages.Infrastructure.Logging
{
    internal class LocalLogMessage
    {
        public DateTime DateTime { get; }

        public String DateTimeFormatted { get { return DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"); } }

        public Exception Exception { get; }

        public object[] FormatParameters { get; }

        public LogLevel Level { get; }

        public Func<string> MessagePredicate { get; }

        public LocalLogMessage(
            LogLevel level,
            Func<string> messagePredicate,
            Exception exception,
            object[] formatParameters)
        {
            DateTime = DateTime.Now;
            Level = level;
            MessagePredicate = messagePredicate;
            Exception = exception;
            FormatParameters = formatParameters;
        }
    }
}