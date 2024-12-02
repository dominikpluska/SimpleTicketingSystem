using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TicketsAPI.Dto;
using TicketsAPI.UnitOfWork;
using TicketsAPI.UnitOfWork.Interface;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IUnitOfWorkTicket _unitOfWorkTicket;
        private readonly IUnitOfWorkCategory _unitOfWorkCategory;
        private readonly IUnitOfWorkComment _unitOfWorkComment;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;
        private readonly Comment _comment;
        
        public TicketsController(IUnitOfWorkTicket unitOfWorkTicket, IUnitOfWorkCategory unitOfWorkCategory,IUnitOfWorkComment unitOfWorkComment, IGlobalServices logService)
        {
            _unitOfWorkTicket = unitOfWorkTicket;
            _unitOfWorkCategory = unitOfWorkCategory;
            _unitOfWorkComment = unitOfWorkComment;
            _logService = logService;

            _response = new Response();
            _log = new Log();
            _comment = new Comment();
        }

        [HttpGet("GetAllTickets")]
        public async Task<ActionResult<Response>> GetAllTickets()
        {
            try
            {
                var list = await _unitOfWorkTicket.TicketRepository.GetAll(includeProperties: "Category");

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
                var ticket = await _unitOfWorkTicket.TicketRepository.Get(x => x.TicketId == id, includeProperties: "Category", tracked: true);
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
        public async  Task<ActionResult<Response>> PostNewTicket(TicketDto ticketDto)
        {
            try
            {
                var category = await _unitOfWorkCategory.CategoryRepository.Get(x => x.CategoryName == ticketDto.CategoryName);

                if (category == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Category must be provided!";
                    _response.Data = null;


                    _log.ServiceName = "TicketAPI";
                    _log.LogType = "Error";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);

                    return StatusCode(500, _response);
                }

                Ticket ticket = new()
                {
                    Title = ticketDto.Title,
                    UserName = ticketDto.UserName,
                    AssigmentGroup = ticketDto.AssigmentGroup,
                    CategoryId = category!.CategoryId,
                    CreationDate = ticketDto.CreationDate,
                    Description = ticketDto.Description,
                    LastModifiedDate = ticketDto.LastModifiedDate,
                    Severity = ticketDto.Severity,
                    Status = ticketDto.Status,
                    TicketType = ticketDto.TicketType,
                };


                _unitOfWorkTicket.TicketRepository.Add(ticket);
                await _unitOfWorkTicket.SaveChanges();
                _response.IsSuccess = true;
                _response.Message = $"New ticket {ticket.TicketId}  has been added by {ticket.UserName}";
                _response.Data = ticket;


                _log.ServiceName = "TicketAPI";
                _log.LogType = "Error";
                _log.UserName = ticket.UserName;
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                if(ticketDto.Comment != null || ticketDto.Comment.Length > 5)
                {

                    _comment.TicketId = ticket.TicketId;
                    _comment.UserId = 1;
                    _comment.CommentField = ticketDto.Comment;

                    _unitOfWorkComment.CommentRepository.Add(_comment);
                    await _unitOfWorkComment.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                _log.ServiceName = "TicketAPI";
                _log.UserName = ticketDto.UserName;
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
                    await _unitOfWorkTicket.SaveChanges();
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
