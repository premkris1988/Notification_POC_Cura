using SendGrid.Helpers.Mail;
using SendGrid;

namespace Notification.Email
{
    public class Notifier<T> : AbstractNotifier<T> where T : EmailMessage
    {
        public override async Task<Guid> SendNotification(T message)
        {
            var messageId = Guid.NewGuid();
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("prem.kris1988@gmail.com", "Test User");
            var subject = message.Title;
            var tos = message.ToAddresses.Select(i => new EmailAddress(i)).ToList(); ;
            var plainTextContent = message.Body;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, string.Empty);
            var response = await client.SendEmailAsync(msg);
            return messageId;
        }
    }
}
