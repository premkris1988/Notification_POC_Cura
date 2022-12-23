namespace Notification.Email
{
    public class EmailMessage : Message
    {
        public List<string> ToAddresses { get; set; } = new List<string>();
    }
}
