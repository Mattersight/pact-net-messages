namespace PactNetMessages
{
    public class PactConfig
    {
        internal string LoggerName;

        public string LogDir { get; set; }

        public string PactDir { get; set; }

        public PactConfig()
        {
            PactDir = Constants.DefaultPactDir;
            LogDir = Constants.DefaultLogDir;
        }
    }
}