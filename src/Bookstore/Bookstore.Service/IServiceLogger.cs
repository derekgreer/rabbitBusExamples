namespace Bookstore.Service
{
    public interface IServiceLogger
    {
        void Write(ServiceLogEntry logEntry);
    }
}