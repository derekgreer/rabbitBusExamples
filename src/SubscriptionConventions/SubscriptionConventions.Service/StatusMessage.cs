namespace SubscriptionConventions.Service
{
	public class StatusMessage
	{
		public StatusMessage(string text)
		{
			Text = text;
		}

		public string Text { get; protected set; }
	}
}