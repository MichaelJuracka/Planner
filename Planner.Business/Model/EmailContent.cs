using Planner.Data.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Business.Model
{
    public class EmailContent
    {
        public string ReceiverEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public EmailAttachment Attachment { get; set; }
    }
}
