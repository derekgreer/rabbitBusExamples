namespace SubscriptionConventions.Service
{
	public interface IServiceLogger
	{
		void Write(ServiceLogEntry logEntry);
	}
}