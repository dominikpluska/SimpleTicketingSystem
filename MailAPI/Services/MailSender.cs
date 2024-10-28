using DataAccess.Models;
using MailAPI.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Services
{
    public class MailSender : IMailSender
    {

        private readonly string _senderMailAddress;
        private readonly string _host;
        private readonly int _port;
        private bool _useDefaultCredentials = true;
        private bool _enableSsl = false;
        private readonly SmtpDeliveryMethod _smtpDeliveryMethod = SmtpDeliveryMethod.Network;
        private readonly string _password;
        private readonly IConfiguration _configuration;


        public MailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _senderMailAddress = _configuration.GetValue<string>("MailSettings:SenderMailAddress");
            _host = _configuration.GetValue<string>("MailSettings:Host");
            _port = _configuration.GetValue<int>("MailSettings:Port");
            _enableSsl = _configuration.GetValue<bool>("EnableSsl:Port");
            _useDefaultCredentials = _configuration.GetValue<bool>("UseDefaultCredentials:Port");
            _password = _configuration.GetValue<string>("MailSettings:Password");
        }


        public void SendEmail(Email email)
        {
            MailAddress sender = new MailAddress(_senderMailAddress);
            MailAddress recipient = new MailAddress(email.RecipientMailAddress);

            NetworkCredential networkCredential = new NetworkCredential(sender.Address, _password);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = _host;
            smtp.Port = _port;
            smtp.UseDefaultCredentials = _useDefaultCredentials;
            smtp.DeliveryMethod = _smtpDeliveryMethod;
            smtp.EnableSsl = _enableSsl;

            if (_useDefaultCredentials == false)
            {
                smtp.Credentials = networkCredential;
            }


            try
            {
                using (var message = new MailMessage(sender, recipient))
                {
                    message.Subject = email.Subject;
                    message.Body = email.Body;

                    if (email.AttachmentPaths != null)
                    {
                        foreach (string filePath in email.AttachmentPaths)
                        {
                            Attachment attachment = new Attachment(filePath);
                            message.Attachments.Add(attachment);
                        }
                    }
                    smtp.Send(message);
                }

            }
            catch (SmtpException ex)
            {
                throw new SmtpException($"Smtp Exception occured! : \n {ex}");
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Format Exception occured : \n {ex}");
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Invali dOperation Exception occured : \n {ex}");
            }
        }
    }
}
