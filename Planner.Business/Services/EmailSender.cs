using MimeKit.Text;
using MimeKit;
using Planner.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string smtpServer = "lin.csad-tisnov.cz";
        private readonly int smtpPort = 587;
        private readonly string userName = "doprava-brno@csad-tisnov.cz";
        private readonly string password = "dc4deldc";
        private const string emailSender = "doprava-brno@csad-tisnov.cz";
        public void SendEmail(string receiverEmail, string subject, string emailBody, string senderEmail = null)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(receiverEmail));
            message.From.Add(new MailboxAddress(senderEmail ?? emailSender));

            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };

            using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
            {
                emailClient.Connect(smtpServer, smtpPort);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(userName, password);

                emailClient.Send(message);

                emailClient.Disconnect(true);
            }
        }
    }
}
