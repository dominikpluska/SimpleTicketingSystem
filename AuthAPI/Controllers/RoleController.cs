using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWorkRole _unitOfWorkRole;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;
        public RoleController(IUnitOfWorkRole unitOfWorkRole, IGlobalServices logService)
        {
            _unitOfWorkRole = unitOfWorkRole;
            _logService = logService;
            _response = new Response();
            _log = new Log();
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<Response>> GetAllRoles()
        {
            try
            {
                var data = await _unitOfWorkRole.RoleRepository.GetAll();

                _response.IsSuccess = true;
                _response.Message = $"Roles has been retrived by string";
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

        [HttpGet("GetRoleById/{id}")]
        public async Task<ActionResult<Response>> GetRoleById(int id)
        {
            try
            {
                var role = await _unitOfWorkRole.RoleRepository.GetFirstOrDefault(x => x.RoleId == id);

                _response.IsSuccess = true;
                _response.Message = $"Role {role.RoleName} has been retrived by string";
                _response.Data = role;

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
    }
}
