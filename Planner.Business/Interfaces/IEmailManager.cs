using Planner.Data.Models.Email;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Interfaces
{
    public interface IEmailManager
    {
        EmailAttachment AddAttachment(string name, byte[] data, int emailId);
        Email AddEmail(DateTime dateSent, string subject, string body, int emailUserId, int emailTemplateId);
        ObservableCollection<EmailTemplate> GetAllEmailTemplates();
        void AddTemplate(string name, string subject, string body);
        EmailTemplate UpdateTemplate(EmailTemplate template, string name, string subject, string body);
        ObservableCollection<EmailUser> GetAllEmailUsers();
        void AddUser(string userName, string password, string smtpServer, string smtpPort, bool isDefault);
        EmailUser UpdateUser(EmailUser user, string userName, string password, string smtpServer, string smtpPort, bool isDefault);
        ObservableCollection<Email> GetAllEmails();
        ObservableCollection<EmailAttachment> GetAllEmailAttachments();
    }
}
