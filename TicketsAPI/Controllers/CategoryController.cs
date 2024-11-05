using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TicketsAPI.UnitOfWork;

namespace TicketsAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //To be continued
        private readonly IUnitOfWorkCategory _unitOfWorkCategory;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;

        public CategoryController(IUnitOfWorkCategory unitOfWorkCategory, IGlobalServices logService)
        {
            _unitOfWorkCategory = unitOfWorkCategory;
            _logService = logService;
            _response = new Response();
            _log = new Log();
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<Response>> GetAllCategories()
        {
            try
            {
                var list = await _unitOfWorkCategory.CategoryRepository.GetAll();
                _response.IsSuccess = true;
                _response.Message = $"All categories have been retrived by the controller";
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
        public async Task<ActionResult<Response>> GetCategoryById(int id)
        {
            try
            {
                var category = await _unitOfWorkCategory.CategoryRepository.GetFirstOrDefault(x => x.CategoryId == id);
                _response.IsSuccess = true;
                _response.Message = $"Category {category.CategoryName} has been retrived from the table";
                _response.Data = category;


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

        [HttpPost("CreateNewCategory")]
        public async Task<ActionResult<Response>> CreateNewCategory(Category category)
        {
            try
            {
                _unitOfWorkCategory.CategoryRepository.Add(category);
                _unitOfWorkCategory.SaveChanges();

                _response.IsSuccess = true;
                _response.Message = $"{category.CategoryName} has been added to the table by string";
                _response.Data = category;

                _log.ServiceName = "TicketsAPI";
                _log.LogType = "Info";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = null;

                _log.ServiceName = "TicketsAPI";
                _log.LogType = "Error";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                return StatusCode(500, _response);
            }

            return _response;
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<Response>> UpdateCategory(int id, Category group)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return _response;
        }   
    }
}
