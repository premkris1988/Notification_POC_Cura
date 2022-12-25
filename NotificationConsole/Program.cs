using Notification;
using Notification.Email;
using Notification.Slack;

namespace NotificationConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("**************Email Notification **********************");
            var emailNotifier = new Notification.Email.Notifier<EmailMessage>();

            var messageId = await emailNotifier.SendNotificationAsync(new EmailMessage
            {
                Body = "Welcome",
                Title = "Hi",
                ToAddresses = new List<string> { "prem.kris1988@gmail.com" }
            });

            Console.WriteLine("**************Slack Notification **********************");
            var slackNotifier = new Notification.Slack.Notifier<SlackMessage>();

            messageId = await slackNotifier.SendNotificationAsync(new SlackMessage
            {
                Body = "Welcome",
                Title = "Hi",
                ChannelName = "Sample Channel"
            });
        }
    }
}