using Planner.Business.Model;
using Planner.Data.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmails(List<EmailContent> emailContents, EmailUser emailUser);
    }
}
