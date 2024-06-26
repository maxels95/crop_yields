public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string htmlContent, string from = null, string plainTextContent = null);
}
