using RabbitBus.Logging;

namespace Bookstore.Service
{
    public class RabbitBusLoggerAdapter : ILogger
    {
        readonly IServiceLogger _logger;

        public RabbitBusLoggerAdapter(IServiceLogger logger)
        {
            _logger = logger;
        }

        public void Write(LogEntry logEntry)
        {
            _logger.Write(new ServiceLogEntry(logEntry.Message, logEntry.Severity));
        }
    }
}