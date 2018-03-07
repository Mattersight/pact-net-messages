using System.Collections.Generic;
using PactNetMessages.Reporters.Outputters;


namespace PactNetMessages
{
    public class PactVerifierConfig
    {
        internal string LoggerName;

        public string LogDir { get; set; }

        public IList<IReportOutputter> ReportOutputters { get; }

        public PactVerifierConfig()
        {
            LogDir = Constants.DefaultLogDir;
            ReportOutputters = new List<IReportOutputter>
            {
                new ConsoleReportOutputter(),
                new FileReportOutputter(() => LoggerName)
            };
        }
    }
}