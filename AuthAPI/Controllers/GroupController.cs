using AuthAPI.UnitOfWork;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IUnitOfWorkGroup _unitOfWorkGroup;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;

        public GroupController(IUnitOfWorkGroup unitOfWorkGroup, IGlobalServices logService)
        {
            _unitOfWorkGroup = unitOfWorkGroup;
            _logService = logService;
            _response = new Response();
            _log = new Log();
        }

        [HttpGet("GetAllGroups")]
        public async Task<ActionResult<Response>> GetAllGroups()
        {
            try
            {
                var data = await _unitOfWorkGroup.GroupRepository.GetAll();

                _response.IsSuccess = true;
                _response.Message = $"Groups has been retrived by string";
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

        [HttpGet("GetGroupById/{id}")]
        public async Task<ActionResult<Response>> GetRoleById(int id)
        {
            try
            {
                var group = await _unitOfWorkGroup.GroupRepository.GetFirstOrDefault(x => x.GroupId == id);

                _response.IsSuccess = true;
                _response.Message = $"{group.GroupName} has been retrived by string";
                _response.Data = group;

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

        [HttpPost("CreateNewGroup")]
        public async Task<ActionResult<Response>> CreateNewGroup(Group group)
        {
            try
            {
                _unitOfWorkGroup.GroupRepository.Add(group);
                await _unitOfWorkGroup.SaveChanges();

                _response.IsSuccess = true;
                _response.Message = $"{group.GroupName} has been added to the table by string";
                _response.Data = group;

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

        [HttpPut("UpdateGroup/{id}")]
        public async Task<ActionResult<Response>> UpdateGroup(int id, Group groupNew)
        {
            try
            {
                var group = await _unitOfWorkGroup.GroupRepository.GetFirstOrDefault(x => x.GroupId == id);
                if (group != null)
                {
                    groupNew.GroupId = group.GroupId;
                    _unitOfWorkGroup.GroupRepository.Update(groupNew);
                    await _unitOfWorkGroup.SaveChanges();

                    _response.IsSuccess = true;
                    _response.Message = $"{group.GroupName} has been modified!";
                    _response.Data = groupNew;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Info";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Group with the id of {id} has not been found!";
                    _response.Data = groupNew;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Info";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                }
                
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

        [HttpDelete("DleteGroup/{id}")]
        public async Task<ActionResult<Response>> DeleteGroup(int id)
        {
            try
            {
                var group = await _unitOfWorkGroup.GroupRepository.GetFirstOrDefault(x => x.GroupId == id);

                if (group != null)
                {
                    _unitOfWorkGroup.GroupRepository.Remove(group);
                    await _unitOfWorkGroup.SaveChanges();

                    _response.IsSuccess = true;
                    _response.Message = $"{group.GroupName} has been removed by string";
                    _response.Data = null;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Info";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Group with the id of  {id}  has not been found!";
                    _response.Data = null;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Info";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);

                }
               
            }
            catch(Exception ex)
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
