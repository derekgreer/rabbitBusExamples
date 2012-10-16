using System.Diagnostics;

namespace SubscriptionConventions.Service
{
	public class ServiceLogEntry
	{
		public ServiceLogEntry(string message, TraceEventType severity)
		{
			Message = message;
			Severity = severity;
		}

		public string Message { get; set; }

		public TraceEventType Severity { get; set; }

		public override string ToString()
		{
			return string.Format("{0}", Message);
		}
	}
}