using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Pinger.Services;

public class EmailNotificationService
{

    public async Task SendAsync(string senderEmail, string toEmail, string subject, string message, string smtpHost, int smtpPort)
    {
        MimeMessage email = new();
        Debug.WriteLine("Sending mail to " + toEmail + " about " + subject);

        email.From.Add(MailboxAddress.Parse(senderEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        email.Body = new TextPart("plain")
        {
            Text = message
        };

        using SmtpClient smtp = new();
        try
        {
            await smtp.ConnectAsync(smtpHost, smtpPort);
            // await smtp.AuthenticateAsync(_senderEmail, _senderPassword);
            var a=smtp.Send(email);
            Debug.WriteLine("Sending result: " + a.ToString());
            await smtp.DisconnectAsync(true);
        }
        catch (SocketException ex)
        {
            Debug.WriteLine(ex.Message.ToString());
        }

    }
}