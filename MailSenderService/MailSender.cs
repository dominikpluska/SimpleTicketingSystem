using MailSenderService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderService
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


        public MailSender(string senderMailAddress, string host, int port = 25)
        {
            _senderMailAddress = senderMailAddress;
            _host = host;
            _port = port;
        }

        public MailSender(string senderMailAddress, string host, bool enableSsl, bool useDefaultCredentials, int port, string password = null)
        {
            _senderMailAddress = senderMailAddress;
            _host = host;
            _port = port;
            _enableSsl = enableSsl;
            _useDefaultCredentials = useDefaultCredentials;
            _password = password;
        }


        public void SendEmail(string recipientMailAddress, string subject, string body, string[] attachmentPaths = null)
        {
            MailAddress sender = new MailAddress(_senderMailAddress);
            MailAddress recipient = new MailAddress(recipientMailAddress);

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
                    message.Subject = subject;
                    message.Body = body;

                    if (attachmentPaths != null)
                    {
                        foreach (string filePath in attachmentPaths)
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
