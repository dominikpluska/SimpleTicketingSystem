using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Sockets;
using TicketsAPI.UnitOfWork;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IUnitOfWorkTicket _unitOfWorkTicket;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;
        
        public TicketsController(IUnitOfWorkTicket unitOfWorkTicket, IGlobalServices logService)
        {
            _unitOfWorkTicket = unitOfWorkTicket;
            _logService = logService;

            _response = new Response();
            _log = new Log();
        }

        [HttpGet("GetAllTickets")]
        public async Task<ActionResult<Response>> GetAllTickets()
        {
            try
            {
                var list = await _unitOfWorkTicket.TicketRepository.GetAll();
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

        [HttpGet("GetTicket/{id}")]
        public async Task<ActionResult<Response>> GetTicket(int id)
        {
            try
            {
                var ticket = await _unitOfWorkTicket.TicketRepository.GetFirstOrDefault(x => x.TicketId == id);
                _response.IsSuccess = true;
                _response.Message = $"Ticket with the Id of {id} tickets has been retrived by the string";
                _response.Data = ticket;


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

        [HttpGet("GetTicketsBasedOnType")]
        public async Task<ActionResult<Response>> GetTicketsBasedOnType(string ticketType)
        {
            try
            {
                var list = await _unitOfWorkTicket.TicketRepository.Get(x => x.TicketType == ticketType);
                _response.IsSuccess = true;
                _response.Message = $"All tickets have been retrived by the controller";
                _response.Data = list;


                _log.ServiceName = "TicketAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
            }
            catch(Exception ex)
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
                _unitOfWorkTicket.TicketRepository.Add(ticket);
                _unitOfWorkTicket.SaveChanges();
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

        [HttpPut("UpdateTicket/{id}")]
        public async Task<ActionResult<Response>> UpdateTicket(int id, Ticket ticketNew)
        {
            try
            {

                var ticket = await _unitOfWorkTicket.TicketRepository.GetFirstOrDefault(x => x.TicketId == id);

                if (ticket != null)
                {
                    ticketNew.TicketId = ticket.TicketId;
                    _unitOfWorkTicket.TicketRepository.Update(ticketNew);
                    _unitOfWorkTicket.SaveChanges();
                    _response.IsSuccess = true;
                    _response.Message = $"New ticket {ticket.TicketId}  has been updated by string";
                    _response.Data = null;

                    _log.ServiceName = "TicketAPI";
                    _log.LogType = "Info";
                    _log.UserName = ticket.UserName;
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);

                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Ticket with the id of {id} has not been found!";
                    _response.Data = "Error";

                    _log.ServiceName = "TicketAPI";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                }

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
    }
}
