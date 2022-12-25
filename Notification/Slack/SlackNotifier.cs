using Newtonsoft.Json;

namespace Notification.Slack
{
    public class Notifier<T> : AbstractNotifier<T> where T : SlackMessage
    {
        public override async Task<NotifierResponse> SendNotificationAsync(T message)
        {
            var messageId = Guid.NewGuid();
            Console.WriteLine("Sending message to {0}", message.ChannelName);
            await Task.Delay(1000);
            await LogNotifications(messageId, JsonConvert.SerializeObject(message), typeof(SlackMessage).Name);
            return new NotifierResponse
            {
                MessageId = messageId,
                Message = "slacked"
            };
        }

        public override async Task<List<NotifierResponse>> SendNotificationsAsync(List<T> messages)
        {
            var responses = new List<NotifierResponse>();
            foreach (var message in messages)
            {
                responses.Add(await SendNotificationAsync(message));
            }
            return responses;
        }
    }
}
