using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TicketsAPI.Repository;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;
        
        public TicketsController(IUnitOfWork unitOfWork, IGlobalServices logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;

            _response = new Response();
            _log = new Log();
        }

        [HttpGet("GetAllTickets")]
        public async Task<ActionResult<Response>> GetAllTickets()
        {
            try
            {
                var list = await _unitOfWork.TicketRepository.GetAll();
                _response.IsSuccess = true;
                _response.Message = $"All tickets have been retrived by the controller";
                _response.Data = list;


                _log.ServiceName = "TicketAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                _log.ServiceName = "TicketAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                return StatusCode(500, _response);
            }

            return _response;
        }

        [HttpPost("PostNewTicket")]
        public ActionResult<Response> PostNewTicket(Ticket ticket)
        {
            try
            {
                _unitOfWork.TicketRepository.Add(ticket);
                _unitOfWork.SaveChanges();
                _response.IsSuccess = true;
                _response.Message = $"New ticket {ticket.TicketId}  has been added by {ticket.UserName}";
                _response.Data = ticket;


                _log.ServiceName = "TicketAPI";
                _log.LogType = "Error";
                _log.UserName = ticket.UserName;
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                _log.ServiceName = "TicketAPI";
                _log.UserName = ticket.UserName;
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                return StatusCode(500, _response);
            }

            return _response;
        }
    }
}
