namespace Infrastructure.Authentication;

public class EmailOptions
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string ConfirmationSubject { get; set; }
}