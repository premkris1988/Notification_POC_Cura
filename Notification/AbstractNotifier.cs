using Microsoft.Extensions.Configuration;

namespace Notification
{
    public abstract class AbstractNotifier<T> : INotifier<T>
    {
        internal string sgKey { get; set; } = string.Empty;
        internal string fromEmail { get; set; } = string.Empty;
        internal string fromAlias { get; set; } = string.Empty;

        public AbstractNotifier()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "EmailConfig.json"));

            var root = builder.Build().GetSection("Configurations");
            sgKey = root["Key"] ?? string.Empty;
            fromEmail = root["FromAddress"] ?? string.Empty;
            fromAlias = root["FromAlias"] ?? string.Empty;


        }
        public abstract Task<Guid> SendNotification(T message);

        public async Task LogNotifications(Guid guid, string message, string type)
        {
            // log the notification to DB
            await Task.Delay(1000);
        }
    }
}
