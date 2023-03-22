using Newtonsoft.Json;

namespace SuperCarga.Api.Logging
{
    public static class LoggerExtensions
    {
        public static void LogData(this ILogger logger, string message, object data) => 
            logger.LogDebug($"{message}, Data: {JsonConvert.SerializeObject(data, Formatting.Indented)}");

    }
}

