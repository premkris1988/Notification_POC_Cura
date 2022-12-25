namespace Notification
{
    public interface INotifier<T>
    {
        Task<NotifierResponse> SendNotificationAsync(T message);

        Task<List<NotifierResponse>> SendNotificationsAsync(List<T> messages);
    }
}