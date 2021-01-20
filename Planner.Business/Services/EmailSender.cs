using MimeKit.Text;
using MimeKit;
using Planner.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Planner.Business.Model;
using Planner.Data.Models.Email;

namespace Planner.Business.Services
{
    public class EmailSender : IEmailSender
    {
        private const string emailSender = "doprava-brno@csad-tisnov.cz";
        public async Task SendEmails(List<EmailContent> emailContents, EmailUser emailUser)
        {
            var messages = new List<MimeMessage>();

            foreach(var content in emailContents)
            {
                var message = new MimeMessage();
                message.To.Add(new MailboxAddress(content.ReceiverEmail));
                message.From.Add(new MailboxAddress(emailUser.UserName ?? emailSender));
                message.Subject = content.Subject;
                var attachment = new MimePart()
                {
                    Content = new MimeContent(new MemoryStream(content.Attachment.Data), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = content.Attachment.Name
                };
                var builder = new BodyBuilder()
                {
                    HtmlBody = content.Body,
                };
                builder.Attachments.Add(attachment);
                message.Body = builder.ToMessageBody();
                messages.Add(message);
            }
            try
            {
                await SendMessages(messages, emailUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task SendMessages(List<MimeMessage> messages, EmailUser emailUser)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(emailUser.SmtpServer, emailUser.SmtpPort);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(emailUser.UserName, emailUser.PassWord);
                foreach (var message in messages)
                    await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
