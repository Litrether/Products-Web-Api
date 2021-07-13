using Contracts;
using NLog;

namespace Products.Managers
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogInfo(string message) =>
            _logger.Info(message);

        public void LogDebug(string message) =>
            _logger.Debug(message);

        public void LogError(string message) =>
            _logger.Error(message);

        public void LogWarn(string message) =>
            _logger.Warn(message);
    }
}
