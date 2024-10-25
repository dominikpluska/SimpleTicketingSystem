using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsAPI.Repository;
using TicketsAPI.Services.IServices;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Response _response;
        private readonly ILogService _logService;
        public TicketsController(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _response = new Response();
            _logService = logService;
        }

        [HttpGet("GetAllLogs")]
        public async Task<ActionResult<Response>> GetAllLogs()
        {
            try
            {
                var list = await _unitOfWork.TicketRepository.GetAll();
                _response.IsSuccess = true;
                _response.Message = $"All tickets have been retrived by the controller";
                _response.Data = list;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";
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

                Log log = new()
                {
                    ServiceName = "TicketAPI",
                    UserName = ticket.UserName,
                    Message = _response.Message
                };

                _logService.WriteLog(log);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                Log log = new()
                {
                    ServiceName = "TicketAPI",
                    UserName = ticket.UserName,
                    Message = _response.Message
                };

                _logService.WriteLog(log);

                return StatusCode(500, _response);
            }

            return _response;
        }
    }



}
