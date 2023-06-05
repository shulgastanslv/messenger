using System.Net;
using System.Net.Mail;
using Application.Common.Abstractions;
using Domain.Entities.Users;
using Domain.Primitives.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication;

public class EmailService : IEmailService
{
    private readonly EmailOptions _options;
    public EmailService(IOptions<EmailOptions> emailOptions)
    {
        _options = emailOptions.Value;
    }

    public async Task<Result> SendEmailAsync(string recipient, string body)
    {
        MailMessage mailMessage = new MailMessage();

        mailMessage.From = new MailAddress(_options.EmailAddress);
        mailMessage.Subject = _options.ConfirmationSubject;

        mailMessage.To.Add(new MailAddress(recipient));

        mailMessage.Body = body;

        using var smtpClient = new SmtpClient(_options.SmtpHost, _options.SmtpPort);

        smtpClient.Credentials = new NetworkCredential(_options.EmailAddress, _options.Password);

        smtpClient.EnableSsl = true;

        await smtpClient.SendMailAsync(mailMessage);

        return Result.Success();
    }
}