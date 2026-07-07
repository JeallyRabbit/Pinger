using System.Diagnostics;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Pinger.Services;

public class EmailNotificationService
{
    private readonly string _smtpHost = "192.168.2.21";
    private readonly int _smtpPort = 25;
    private readonly string _senderEmail = "no-replyCR@vcorrect.eu";
    //private readonly string _senderPassword = "your-app-password";

    public async Task SendAsync(string toEmail, string subject, string message)
    {
        MimeMessage email = new();
        Debug.WriteLine("Sending mail to "+toEmail+" about "+subject);

        email.From.Add(MailboxAddress.Parse(_senderEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        email.Body = new TextPart("plain")
        {
            Text = message
        };

        using SmtpClient smtp = new();

        await smtp.ConnectAsync(_smtpHost, _smtpPort);
       // await smtp.AuthenticateAsync(_senderEmail, _senderPassword);
        var a =  smtp.Send(email);
       // await a;
        await smtp.DisconnectAsync(true);
    }
}