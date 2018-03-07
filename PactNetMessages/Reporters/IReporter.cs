using PactNetMessages.Comparers;


namespace PactNetMessages.Reporters
{
    internal interface IReporter
    {
        void ReportInfo(string infoMessage);

        void ReportSummary(ComparisonResult comparisonResult);

        void ReportFailureReasons(ComparisonResult comparisonResult);

        void Indent();

        void ResetIndentation();

        void Flush();
    }
}