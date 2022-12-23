using SendGrid.Helpers.Mail;
using SendGrid;
using Newtonsoft.Json;

namespace Notification.Email
{
    public class Notifier<T> : AbstractNotifier<T> where T : EmailMessage
    {
        public override async Task<Guid> SendNotification(T message)
        {
            var messageId = Guid.NewGuid();
            var client = new SendGridClient(sgKey);
            var from = new EmailAddress(fromEmail, fromAlias);
            var subject = message.Title;
            var tos = message.ToAddresses.Select(i => new EmailAddress(i)).ToList();
            var plainTextContent = message.Body;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, string.Empty);
            var response = await client.SendEmailAsync(msg);
            await LogNotifications(messageId, JsonConvert.SerializeObject(message), typeof(EmailMessage).Name);
            return messageId;
        }
    }
}
