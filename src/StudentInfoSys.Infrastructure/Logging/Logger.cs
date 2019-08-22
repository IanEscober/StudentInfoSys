namespace StudentInfoSys.Infrastructure.Logging
{
    using Microsoft.Extensions.Logging;
    using StudentInfoSys.Domain.Interfaces.Logging;

    public class Logger<T> : IBaseLogger<T>
    {
        private readonly ILogger<T> logger;

        public Logger(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<T>();
        }

        public void LogInfo(string message, params object[] args)
        {
            this.logger.LogInformation(message, args);
        }

        public void LogWarn(string message, params object[] args)
        {
            this.logger.LogWarning(message, args);
        }
    }
}
