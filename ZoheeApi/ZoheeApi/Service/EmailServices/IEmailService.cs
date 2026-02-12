namespace FIN.Service.EmailServices
{
    public interface IEmailService
    {
        // Creates an email service to allow sending emails
        public Task EMailAsync(string toEmail, string subject, string htmlMessage);

        // Sends an email verification email
        public Task SendSignatureRequestEmailAsync(string email, string documentName, int documentId, int userId);

        // Sends a forgotten password email
        public Task SendDocumentSignedNotificationAsync(string adminEmail, string signeeName, string documentName);
    }
}
