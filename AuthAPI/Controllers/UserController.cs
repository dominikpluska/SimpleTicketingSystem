using AuthAPI.JwtGenerator.ICreateJwtToken;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkAuth _unitOfWorkAuth;
        private readonly IUnitOfWorkUser _unitOfWorkUser;
        private readonly ICreateJwtToken _createJwtToken;
        private readonly IUnitOfWorkRole _unitOfWorkRole;
        private readonly IUnitOfWorkGroup _unitOfWorkGroup;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;

        public UserController(IUnitOfWorkAuth unitOfWorkAuth, IUnitOfWorkUser unitOfWorkUser, ICreateJwtToken createJwtToken,
            IUnitOfWorkRole unitOfWorkRole, IUnitOfWorkGroup unitOfWorkGroup, IGlobalServices logService)
        {
            _unitOfWorkAuth = unitOfWorkAuth;
            _unitOfWorkUser = unitOfWorkUser;
            _unitOfWorkRole = unitOfWorkRole;
            _unitOfWorkGroup = unitOfWorkGroup;
            _createJwtToken = createJwtToken;
            _logService = logService;
            _log = new Log();
            _response = new Response();
        }

        [HttpPost("RegisterNewUser")]
        public async Task<ActionResult<Response>> RegisterNewUser(User user)
        {
            try
            {
                _unitOfWorkUser.UserRepository.Add(user);

                _response.IsSuccess = true;
                _response.Message = $"User {user.Email} added to the databse";
                _response.Data = null;

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Info";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

                await _unitOfWorkUser.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = null;

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Error";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
                return StatusCode(500, _response);
            }
            return _response;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<Response>> GetAllUsers()
        {
            try
            {
                var data = await _unitOfWorkUser.UserRepository.GetAll();

                _response.IsSuccess = true;
                _response.Message = $"Data of users retrived from the database";
                _response.Data = data;

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Info";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = null;

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Error";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
                return StatusCode(500, _response);
            }

            return _response;
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<Response>> GetUserById(int id)
        {
            try
            {
                var data = await _unitOfWorkUser.UserRepository.GetFirstOrDefault(x => x.UserId == id);

                _response.IsSuccess = true;
                _response.Message = $"Data of {data.Email} retrived from the database";
                _response.Data = data;

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Info";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = null;

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Error";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
                return StatusCode(500, _response);
            }
            return _response;
        }

        [HttpPost("UpdateUser/{id}")]
        public async Task<ActionResult<Response>> UpdateUser(int id, User userNew)
        {
            try
            {
                var user = await _unitOfWorkUser.UserRepository.GetFirstOrDefault(x => x.UserId == id);
                if (user != null)
                {
                    userNew.UserId = id;
                    _unitOfWorkUser.UserRepository.Update(user);
                    await _unitOfWorkUser.SaveChanges();

                    _response.IsSuccess = true;
                    _response.Message = $"{userNew.Name} has been updated!";
                    _response.Data = userNew;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Info";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = $"User with the id of {id} has not been found!";
                    _response.Data = userNew;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Error";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
                _response.Data = userNew;

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Error";
                _log.UserName = "string";
                _log.Message = _response.Message;

                _logService.WriteLog(_log);
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
