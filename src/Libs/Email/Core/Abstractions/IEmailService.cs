namespace Communication.Email.Core.Abstractions
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}
