using Newtonsoft.Json;


namespace PactNetMessages.Configuration.Json
{
    internal static class JsonConfig
    {
        private static JsonSerializerSettings _apiRequestSerializerSettings;

        private static JsonSerializerSettings _serializerSettings;

        internal static JsonSerializerSettings ApiSerializerSettings
        {
            get
            {
                _apiRequestSerializerSettings = _apiRequestSerializerSettings ?? new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.None
                };
                return _apiRequestSerializerSettings;
            }
            set { _apiRequestSerializerSettings = value; }
        }

        internal static JsonSerializerSettings PactFileSerializerSettings
        {
            get
            {
                _serializerSettings = _serializerSettings ?? new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                return _serializerSettings;
            }
        }
    }
}