using SendGrid.Helpers.Mail;
using SendGrid;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Notification.Email
{
    public class Notifier<T> : AbstractNotifier<T>
        where T : EmailMessage

    {
        private (string sgKey, string fromEmail, string fromAlias)? config;
        private (string sgKey, string fromEmail, string fromAlias) GetConfigurations()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "EmailConfig.json"));

            var root = builder.Build().GetSection("Configurations");
            var sgKey = root["Key"] ?? string.Empty;
            var fromEmail = root["FromAddress"] ?? string.Empty;
            var fromAlias = root["FromAlias"] ?? string.Empty;
            return (sgKey, fromEmail, fromAlias);
        }

        public override async Task<NotifierResponse> SendNotificationAsync(T message)
        {
            config ??= GetConfigurations();
            var messageId = Guid.NewGuid();
            var client = new SendGridClient(config?.sgKey);
            var from = new EmailAddress(config?.fromEmail, config?.fromAlias);
            var subject = message.Title;
            var tos = message.ToAddresses.Select(i => new EmailAddress(i)).ToList();
            var plainTextContent = message.Body;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, string.Empty);
            var response = await client.SendEmailAsync(msg);
            await LogNotifications(messageId, JsonConvert.SerializeObject(message), typeof(EmailMessage).Name);
            return new NotifierResponse
            {
                MessageId = messageId,
                Message = JsonConvert.SerializeObject(await response.DeserializeResponseBodyAsync())
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
