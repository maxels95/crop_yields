using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridEmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly SendGridClient _client;

    public SendGridEmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        _client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
    }

    public async Task SendEmailAsync(string to, string subject, string htmlContent, string from = null, string plainTextContent = null)
    {
        var fromEmail = new EmailAddress(from ?? _configuration["SendGrid:DefaultFromEmail"], "Your Application Name");
        var toEmail = new EmailAddress(to);
        var message = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, plainTextContent, htmlContent);

        var response = await _client.SendEmailAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to send email: {response.StatusCode}");
        }
    }
}
