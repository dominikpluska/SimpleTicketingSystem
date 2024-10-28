using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Services.IServices
{
    public interface IMailSender
    {
        void SendEmail(Email email);
    }
}
