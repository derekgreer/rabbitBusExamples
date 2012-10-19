using System.Diagnostics;

namespace Bookstore.Service
{
    public static class ServiceLoggerExtensions
    {
        public static void Write(this IServiceLogger serviceLogger, string message)
        {
            serviceLogger.Write(new ServiceLogEntry(message, TraceEventType.Information));
        }

        public static void Write(this IServiceLogger serviceLogger, string message, TraceEventType traceEventType)
        {
            serviceLogger.Write(new ServiceLogEntry(message, traceEventType));
        }
    }
}