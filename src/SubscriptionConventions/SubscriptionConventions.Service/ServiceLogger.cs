namespace SubscriptionConventions.Service
{
	public static class ServiceLogger
	{
		public static IServiceLogger Current { get; private set; }

		public static void SetCurrentLogger(IServiceLogger serviceLogger)
		{
			Current = serviceLogger;
		}
	}
}