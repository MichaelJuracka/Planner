using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models.Email;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Managers
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailRepository emailRepository;
        private readonly IEmailAttachmentRepository emailAttachmentRepository;
        private readonly IEmailTemplateRepository emailTemplateRepository;
        private readonly IEmailUserRepository emailUserRepository;
        public EmailManager(
            IEmailRepository emailRepository,
            IEmailAttachmentRepository emailAttachmentRepository,
            IEmailTemplateRepository emailTemplateRepository,
            IEmailUserRepository emailUserRepository)
        {
            this.emailRepository = emailRepository;
            this.emailAttachmentRepository = emailAttachmentRepository;
            this.emailTemplateRepository = emailTemplateRepository;
            this.emailUserRepository = emailUserRepository;
        }
        public EmailAttachment AddAttachment(string name, byte[] data, int emailId)
        {
            var attachment = new EmailAttachment()
            {
                Name = name,
                Data = data,
                EmailId = emailId
            };
            emailAttachmentRepository.Insert(attachment);

            return attachment;
        }
        public Email AddEmail(DateTime dateSent, string subject, string body, int emailUserId, int emailTemplateId)
        {
            var email = new Email()
            {
                DateSent = dateSent,
                Subject = subject,
                Body = body,
                EmailUserId = emailUserId,
                EmailTemplateId = emailTemplateId,
            };
            emailRepository.Insert(email);
            return email;
        }
        public ObservableCollection<EmailTemplate> GetAllEmailTemplates()
        {
            return emailTemplateRepository.GetAll();
        }
        public ObservableCollection<EmailUser> GetAllEmailUsers()
        {
            return emailUserRepository.GetAll();
        }
        public ObservableCollection<Email> GetAllEmails()
        {
            return emailRepository.GetAll();
        }
        public ObservableCollection<EmailAttachment> GetAllEmailAttachments()
        {
            return emailAttachmentRepository.GetAll();
        }
        public void AddTemplate(string name, string subject, string body)
        {
            if (name.Length == 0)
                throw new ArgumentException("Vyplňte název šablony");
            if (subject.Length == 0)
                throw new ArgumentException("Vyplňte předmět šablony");
            if (body.Length == 0)
                throw new ArgumentException("Vyplňte tělo šablony");

            var template = new EmailTemplate()
            {
                Name = name,
                Subject = subject,
                Body = body
            };

            emailTemplateRepository.Insert(template);
        }
        public EmailTemplate UpdateTemplate(EmailTemplate template, string name, string subject, string body)
        {
            if (name.Length == 0)
                throw new ArgumentException("Vyplňte název šablony");
            if (subject.Length == 0)
                throw new ArgumentException("Vyplňte předmět šablony");
            if (body.Length == 0)
                throw new ArgumentException("Vyplňte tělo šablony");

            template.Name = name;
            template.Subject = subject;
            template.Body = body;

            emailTemplateRepository.Update(template);
            return template;
        }
        public void AddUser(string userName, string password, string smtpServer, string smtpPort, bool isDefault)
        {
            if (userName.Length == 0)
                throw new ArgumentException("Vyplňte email");
            if (password.Length == 0)
                throw new ArgumentException("Vyplňte heslo emailu");
            if (smtpServer.Length == 0)
                throw new ArgumentException("Vyplňte Smtp Server");
            if (smtpPort.Length == 0)
                throw new ArgumentException("Vyplňte Smtp Port");
            if (isDefault && emailUserRepository.GetDefaultUser() != null)
                emailUserRepository.GetDefaultUser().Default = false;

            var user = new EmailUser()
            {
                UserName = userName,
                PassWord = password,
                SmtpServer = smtpServer,
                SmtpPort = int.Parse(smtpPort),
                Default = isDefault
            };

            emailUserRepository.Insert(user);
        }
        public EmailUser UpdateUser(EmailUser user, string userName, string password, string smtpServer, string smtpPort, bool isDefault)
        {
            if (userName.Length == 0)
                throw new ArgumentException("Vyplňte email");
            if (password.Length == 0)
                throw new ArgumentException("Vyplňte heslo emailu");
            if (smtpServer.Length == 0)
                throw new ArgumentException("Vyplňte Smtp Server");
            if (smtpPort.Length == 0)
                throw new ArgumentException("Vyplňte Smtp Port");
            if (isDefault)
                emailUserRepository.GetDefaultUser().Default = false;

            user.UserName = userName;
            user.PassWord = password;
            user.SmtpServer = smtpServer;
            user.SmtpPort = int.Parse(smtpPort);
            user.Default = isDefault;

            emailUserRepository.Update(user);
            return user;
        }
    }
}
