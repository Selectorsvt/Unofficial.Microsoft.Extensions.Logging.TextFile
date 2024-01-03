using Microsoft.Extensions.Logging;

namespace Unofficial.Microsoft.Extensions.Logging.TextFile
{
    public sealed class FileLoggerConfiguration
    {
        public Dictionary<LogLevel, string> FileMap { get; set; } = new()
        {
        };
    }

}
