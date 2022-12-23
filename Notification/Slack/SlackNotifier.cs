namespace Notification.Slack
{
    public class Notifier<T> : AbstractNotifier<T> where T : SlackMessage
    {
        public override async Task<Guid> SendNotification(T message)
        {
            var messageId = Guid.NewGuid();
            Console.WriteLine("Sending message to {0}", message.ChannelName);
            await Task.Delay(1000);
            return messageId;
        }
    }
}
