using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Mvc;
using TicketsAPI.UnitOfWork;
using TicketsAPI.UnitOfWork.Interface;

namespace TicketsAPI.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWorkComment _unitOfWorkComment;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;

        public CommentController(IUnitOfWorkComment unitOfWorkComment, IGlobalServices globalServices)
        {
            _unitOfWorkComment = unitOfWorkComment;
            _logService = globalServices;
            _response = new Response();
            _log = new Log();
        }

        [HttpGet("GetAllComments")]
        public async Task<ActionResult<Response>> GetAllComments()
        {
            try
            {
                var list = await _unitOfWorkComment.CommentRepository.GetAll();

                _response.IsSuccess = true;
                _response.Message = $"All comments have been retrived by the controller";
                _response.Data = list;
                _log.ServiceName = "CommentAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;
                _logService.WriteLog(_log);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                _log.ServiceName = "CommentAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                return StatusCode(500, _response);
            }

            return _response;
        }

        [HttpGet("GetTicketComments/{ticketId}")]
        public async Task<ActionResult<Response>> GetTicketComments(int ticketId)
        {
            try
            {
                var list = await _unitOfWorkComment.CommentRepository.GetAll(x => x.TicketId == ticketId);

                _response.IsSuccess = true;
                _response.Message = $"Comments of {ticketId} have been retrived by the controller";
                _response.Data = list;
                _log.ServiceName = "CommentAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;
                _logService.WriteLog(_log);
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                _log.ServiceName = "CommentAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                return StatusCode(500, _response);
            }

            return _response;
        }

        [HttpGet("GetComment/{id}")]
        public async Task<ActionResult<Response>> GetComment(int id)
        {
            try
            {
                var comment = await _unitOfWorkComment.CommentRepository.Get(x => x.TicketId == id);

                _response.IsSuccess = true;
                _response.Message = $"Comment{id} has been retrived by the controller";
                _response.Data = comment;
                _log.ServiceName = "CommentAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;
                _logService.WriteLog(_log);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = "Error";

                _log.ServiceName = "CommentAPI";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                return StatusCode(500, _response);
            }

            return _response;
        }
    }
}
