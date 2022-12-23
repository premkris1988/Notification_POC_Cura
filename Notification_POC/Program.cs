using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification;
using Notification.Email;
using Notification.Slack;
using static System.Formats.Asn1.AsnWriter;

namespace Notification_POC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                                        .ConfigureServices((_, services) =>
                                                 services
                                                     .AddScoped<INotifier<EmailMessage>, Notification.Email.Notifier<EmailMessage>>()
                                                     .AddScoped<INotifier<SlackMessage>, Notification.Slack.Notifier<SlackMessage>>())
                                        .Build();



            Console.WriteLine("*******************************************");
            await SendEmail(host.Services);


            Console.WriteLine("*******************************************");
            Console.WriteLine("*******************************************");

            await SendSlackMessage(host.Services);

            Console.WriteLine("*******************************************");

            await host.RunAsync();
        }

        private static async Task SendSlackMessage(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var slackNotifier = provider.GetRequiredService<INotifier<SlackMessage>>();

            var messageId = await slackNotifier.SendNotification(new SlackMessage
            {
                Body = "Welcome",
                Title = "Hi",
                ChannelName = "Sample Channel"
            });
            Console.WriteLine("Message sent -{0}", messageId);
        }

        private static async Task SendEmail(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var emailNotifier = provider.GetRequiredService<INotifier<EmailMessage>>();

            var messageId = await emailNotifier.SendNotification(new EmailMessage
            {
                Body = "Welcome",
                Title = "Hi",
                ToAddresses = new List<string> { "prem.kris1988@gmail.com" }
            });
            Console.WriteLine("Message sent -{0}", messageId);
        }
    }
}