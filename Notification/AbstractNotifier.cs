namespace Notification
{
    public abstract class AbstractNotifier<T> : INotifier<T>
    {
        public abstract Task<Guid> SendNotification(T message);
    }
}
