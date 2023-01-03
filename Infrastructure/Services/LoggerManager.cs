using Infrastructure.Services.Interfaces;
using NLog;

namespace Infrastructure.Services
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug("Admin - " + message);
        }

        public void LogError(string message)
        {
            logger.Error("Admin - " + message);
        }

        public void LogInfo(string message)
        {
            logger.Info("Admin - " + message);
        }

        public void LogWarn(string message)
        {
            logger.Warn("Admin - " + message);
        }
    }
}
