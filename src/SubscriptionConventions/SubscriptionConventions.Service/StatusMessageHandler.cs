using RabbitBus;

namespace SubscriptionConventions.Service
{
	public class StatusMessageHandler : IMessageHandler<StatusMessage>
	{
		readonly IStatusRepository _statusRepository;

		public StatusMessageHandler(IStatusRepository statusRepository)
		{
			_statusRepository = statusRepository;
		}

		public void Handle(IMessageContext<StatusMessage> messageContext)
		{
			_statusRepository.Save(messageContext.Message);
			
		}
	}

	public interface IStatusRepository
	{
		void Save(StatusMessage statusMessage);
	}

	class StatusRepository : IStatusRepository
	{
		public void Save(StatusMessage statusMessage)
		{
			ServiceLogger.Current.Write(statusMessage.Text);
		}
	}
}