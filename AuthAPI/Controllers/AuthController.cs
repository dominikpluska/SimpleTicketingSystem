using AuthAPI.Dto;
using AuthAPI.JwtGenerator.ICreateJwtToken;
using AuthAPI.UnitOfWork;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWorkAuth _unitOfWorkAuth;
        private readonly IUnitOfWorkUser _unitOfWorkUser;
        private readonly ICreateJwtToken _createJwtToken;
        private readonly IUnitOfWorkRole _unitOfWorkRole;
        private readonly IGlobalServices _logService;
        private readonly Response _response;
        private readonly Log _log;

        public AuthController(IUnitOfWorkAuth unitOfWorkAuth, IUnitOfWorkUser unitOfWorkUser, ICreateJwtToken createJwtToken,
            IUnitOfWorkRole unitOfWorkRole, IGlobalServices logService)
        {
            _unitOfWorkAuth = unitOfWorkAuth;
            _unitOfWorkUser = unitOfWorkUser;
            _unitOfWorkRole = unitOfWorkRole;
            _createJwtToken = createJwtToken;
            _logService = logService;
            _log = new Log();
            _response = new Response();
        }

        [HttpPost("RegisterNewAccount")]
        public async Task<ActionResult<Response>> RegisterNewAccount(UserAccountDto userAccountDto)
        {
            try
            {
                User user = await _unitOfWorkUser.UserRepository.GetFirstOrDefault(x => x.Email == userAccountDto.Email);

                if (user == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Specified Email does not occur in the User Table. Please add User reference before creating an account";
                    _response.Data = null;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Error";
                    _log.UserName = "string";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                }
                else
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(userAccountDto.Password);

                    UserAccount userAccount = new();
                    userAccount.UserId = user.UserId;
                    userAccount.PasswordHash = passwordHash;
                    userAccount.UserName = userAccountDto.UserName;
                    userAccount.RoleId  = await _unitOfWorkRole.RoleRepository.GetRoleId();

                    _unitOfWorkAuth.AuthRepository.Add(userAccount);
                }

                


            }
            catch (Exception ex)
            {
                return _response;
            }

            return _response;
        }

    }
}
