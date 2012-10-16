using System.Collections.Generic;
using System.Diagnostics;
using log4net;

namespace SubscriptionConventions.Service
{
	delegate void LogWriterProvider(ServiceLogEntry logEntry);

	public class Log4NetLogger : IServiceLogger
	{
		const string DefaultLogger = "Default";
		static readonly ILog Log = LogManager.GetLogger(DefaultLogger);
		readonly Dictionary<TraceEventType, LogWriterProvider> _map;

		public Log4NetLogger()
		{
			_map =
				new Dictionary<TraceEventType, LogWriterProvider>
					{
						{TraceEventType.Warning, e => Log.Warn(e.ToString())},
						{TraceEventType.Verbose, e => Log.Debug(e.ToString())},
						{TraceEventType.Transfer, e => Log.Debug(e.ToString())},
						{TraceEventType.Suspend, e => Log.Debug(e.ToString())},
						{TraceEventType.Stop, e => Log.Debug(e.ToString())},
						{TraceEventType.Start, e => Log.Debug(e.ToString())},
						{TraceEventType.Resume, e => Log.Debug(e.ToString())},
						{TraceEventType.Information, e => Log.Info(e.ToString())},
						{TraceEventType.Error, e => Log.Error(e.ToString())},
						{TraceEventType.Critical, e => Log.Fatal(e.ToString())},
					};
		}

		public void Write(ServiceLogEntry logEntry)
		{
			TraceEventType TraceEventType = logEntry.Severity;
			if (_map.ContainsKey(TraceEventType))
			{
				_map[TraceEventType](logEntry);
			}
		}
	}
}