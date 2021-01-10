using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string receiverEmail, string subject, string emailBody, string senderEmail = null);
    }
}
