using AuthAPI.CookieGenerator;
using AuthAPI.Data.Dtos;
using AuthAPI.Dto;
using AuthAPI.JwtGenerator.ICreateJwtToken;
using AuthAPI.UnitOfWork;
using AuthAPI.UnitOfWork.Interfaces;
using BCrypt.Net;
using DataAccess.Models;
using GlobalServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        private readonly IUnitOfWorkGroup _unitOfWorkGroup;
        private readonly IUnitOfWorkJwt _unitOfWorkJwt;
        private readonly IGlobalServices _logService;
        private readonly ICookieGenerator _cookieGenerator;
        private readonly Response _response;
        private readonly Log _log;

        public AuthController(IUnitOfWorkAuth unitOfWorkAuth, IUnitOfWorkUser unitOfWorkUser, ICreateJwtToken createJwtToken,
            IUnitOfWorkRole unitOfWorkRole, IUnitOfWorkGroup unitOfWorkGroup, IUnitOfWorkJwt unitOfWorkJwt, IGlobalServices logService, ICookieGenerator cookieGenerator)
        {
            _unitOfWorkAuth = unitOfWorkAuth;
            _unitOfWorkUser = unitOfWorkUser;
            _unitOfWorkRole = unitOfWorkRole;
            _unitOfWorkGroup = unitOfWorkGroup;
            _unitOfWorkJwt = unitOfWorkJwt;
            _createJwtToken = createJwtToken;
            _logService = logService;
            _cookieGenerator = cookieGenerator;
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
                    return StatusCode(500, _response);
                }
                else
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(userAccountDto.Password);

                    UserAccount userAccount = new();
                    userAccount.UserId = user.UserId;
                    userAccount.PasswordHash = passwordHash;
                    userAccount.UserName = userAccountDto.UserName;

                    if (string.IsNullOrEmpty(userAccountDto.SelectedRole))
                    {
                        _response.IsSuccess = false;
                        _response.Message = $"User Account must have specified Role!";
                        _response.Data = null;

                        _log.ServiceName = "AuthAPI";
                        _log.LogType = "Error";
                        _log.UserName = "string";
                        _log.Message = _response.Message;

                        _logService.WriteLog(_log);
                        return StatusCode(500, _response);
                    }

                    userAccount.RoleId  = await _unitOfWorkRole.RoleRepository.GetRoleId(userAccountDto.SelectedRole);

                    if(string.IsNullOrEmpty(userAccountDto.SelectedGroup))
                    {
                        _response.IsSuccess = false;
                        _response.Message = $"User Account must have specified Group!";
                        _response.Data = null;

                        _log.ServiceName = "AuthAPI";
                        _log.LogType = "Error";
                        _log.UserName = "string";
                        _log.Message = _response.Message;

                        _logService.WriteLog(_log);
                        return StatusCode(500, _response);
                    }

                    userAccount.GroupId = await _unitOfWorkGroup.GroupRepository.GetGroupId(userAccountDto.SelectedGroup);

                    _unitOfWorkAuth.AuthRepository.Add(userAccount);
                    _unitOfWorkAuth.SaveChanges();


                    _response.IsSuccess = true;
                    _response.Message = $"User account {userAccount.UserName} has been added to the database";
                    _response.Data = null;

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

        [HttpPost("Login")]
        public async Task<ActionResult<Response>> Login(UserAccountDto userAccountDto)
        {
            try
            {
                UserAccount userAccount = new();
                Role role = new();

                if (!_unitOfWorkAuth.AuthRepository.CheckIfUserExists(userAccountDto.UserName))
                {
                    _response.IsSuccess = false;
                    _response.Message = $"User Or Password was Incorrect!";
                    _response.Data = null;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Error";
                    _log.UserName = "AuthAPI";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                    return StatusCode(500, _response);
                }
                else
                {
                    userAccount = await _unitOfWorkAuth.AuthRepository.GetFirstOrDefault(x => x.UserName == userAccountDto.UserName);
                    role = await _unitOfWorkRole.RoleRepository.GetFirstOrDefault(x => x.RoleId == userAccount.RoleId);
                }

                if (!BCrypt.Net.BCrypt.Verify(userAccountDto.Password, userAccount!.PasswordHash))
                {
                    _response.IsSuccess = false;
                    _response.Message = $"User Or Password was Incorrect!";
                    _response.Data = null;

                    _log.ServiceName = "AuthAPI";
                    _log.LogType = "Error";
                    _log.UserName = "AuthAPI";
                    _log.Message = _response.Message;

                    _logService.WriteLog(_log);
                    return StatusCode(500, _response);
                }

                else
                {
                    JwtDto jwtDto = new JwtDto()
                    {
                        Username = userAccount.UserName,
                        Role = role.RoleName,

                    };
                    var token = _createJwtToken.GenerateToken(jwtDto);
                    _cookieGenerator.CreateCookie(token, Response);

                    JWT jwt = new();
                    jwt.UserId = userAccount.UserId;
                    jwt.JwtToken = token;

                    _unitOfWorkJwt.JwtRepository.Add(jwt);

                    _response.IsSuccess = true;
                    _response.Message = $"User {userAccountDto.UserName} has been logged";
                    _response.Data = token;
                }

                _log.ServiceName = "AuthAPI";
                _log.LogType = "Info";
                _log.UserName = userAccountDto.UserName;
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
