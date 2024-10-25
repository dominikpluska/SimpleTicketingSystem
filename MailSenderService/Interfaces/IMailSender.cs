using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderService.Interfaces
{
    public interface IMailSender
    {
        void SendEmail(string recipientMailAddress, string subject, string body, string[] attachmentPaths = null);
    }
}
