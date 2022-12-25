using Microsoft.Extensions.Configuration;

namespace Notification
{
    public abstract class AbstractNotifier<T> : INotifier<T>
    {
        public abstract Task<NotifierResponse> SendNotificationAsync(T message);

        public async Task LogNotifications(Guid guid, string message, string type)
        {
            // log the notification to DB
            Console.WriteLine("{0} - {1} - {2}", guid, message, type);
            await Task.Delay(1000);
        }

        public abstract Task<List<NotifierResponse>> SendNotificationsAsync(List<T> messages);
    }
}
