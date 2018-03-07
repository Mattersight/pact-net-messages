using System;


namespace PactNetMessages.Reporters.Outputters
{
    internal class ConsoleReportOutputter : IReportOutputter
    {
        public void Write(string report)
        {
            Console.WriteLine(report);
        }
    }
}