namespace Notification
{
    public interface INotifier<T>
    {
        Task<Guid> SendNotification(T message);
    }
}