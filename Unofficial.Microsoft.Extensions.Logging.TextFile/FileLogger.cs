using Microsoft.Extensions.Logging;

namespace Unofficial.Microsoft.Extensions.Logging.TextFile
{
    public sealed class FileLogger(string name, Func<FileLoggerConfiguration> getCurrentConfig) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) => getCurrentConfig().FileMap.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            string filePath = GetFilePath(logLevel);
            System.IO.File.AppendAllText(filePath, $"[{eventId.Id,2}: {logLevel,-12}] {name} - {formatter(state, exception)} {Environment.NewLine}");
        }

        private string GetFilePath(LogLevel logLevel)
        {
            FileLoggerConfiguration config = getCurrentConfig();
            var fileName = config.FileMap[logLevel];
            fileName = Path.IsPathRooted(fileName) ? fileName : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            var folder = Path.GetDirectoryName(fileName);
            #if NET7_0_OR_GREATER
                 ArgumentException.ThrowIfNullOrEmpty(folder);
            #else
                 if(folder == null)
                    throw new ArgumentException("Incorrect folder name", nameof(folder));
            #endif

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return fileName;
        }
    }
}
