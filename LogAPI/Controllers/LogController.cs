using DataAccess.Models;
using LogAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace LogAPI.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Response _response;
        public LogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new Response();
        }
        [HttpGet("GetAllLogs")]
        public async Task<ActionResult<Response>> GetAllLogs()
        {
            try
            {
                var list = await _unitOfWork.LogRepository.GetAll();
                _response.IsSuccess = true;
                _response.Message = $"All logs have been retrived by the controller";
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

        [HttpGet("GetAllLogsByServiceName")]
        public async Task<ActionResult<Response>> GetAllLogsByServiceName(string serviceName)
        {
            try
            {
                var list = await _unitOfWork.LogRepository.GetAll(x => x.ServiceName == serviceName);
                _response.IsSuccess = true;
                _response.Message = $"Logs of {serviceName} retrived by the controller";
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

        [HttpPost("WriteLog")]
        public ActionResult<Response> WriteLog(Log log)
        {
            try
            {
                _unitOfWork.LogRepository.Add(log);
                _unitOfWork.SaveChanges();
                _response.IsSuccess = true;
                _response.Message = "Log Added";
                _response.Data = log;
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
    }
}
