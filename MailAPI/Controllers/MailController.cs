using DataAccess.Models;
using GlobalServices.Interface;
using MailAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailSender _mailSender;
        private readonly IGlobalServices _logService;

        private readonly Response _response;
        private readonly Log _log;

        public MailController(IMailSender mailSender, IGlobalServices globalServices)
        {
            _mailSender = mailSender;
            _logService = globalServices;

            _response = new Response();
            _log = new Log();
        }

        [HttpPost("SendEmail")]
        public ActionResult<Response> SendEmail(Email email)
        {
            try
            {
                _mailSender.SendEmail(email);
                _response.IsSuccess = true;
                _response.Message = $"Email has been sent to {email.RecipientMailAddress}";
                _response.Data = email.Body;

                _log.ServiceName = "MailApi";
                _log.UserName = "MailService";
                _log.Message = _response.Message;
                _logService.WriteLog(_log);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                _log.ServiceName = "MailApi";
                _log.LogType = "Error";
                _log.UserName = "MailService";
                _log.Message = _response.Message;
                _logService.WriteLog(_log);

                return StatusCode(500, _response);
            }
            return _response;
            
        }

    }
}
