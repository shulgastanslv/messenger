using System.Net.Mail;
using Domain.Primitives.Result;

namespace Application.Common.Abstractions;

public interface IEmailService
{
    Task<Result> SendEmailAsync(string recipient, string body);
}