using AuthAPI.JwtGenerator.ICreateJwtToken;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using DataAccess.Models;
using System.Security.Claims;
using System.Data;
using AuthAPI.Data.Dtos;

namespace AuthAPI.JwtGenerator
{
    public class CreateJwtToken : ICreateJwtToken.ICreateJwtToken
    {
        private readonly string _tokenString;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly DateTime _expiryTime;
        private readonly List<Claim> _claims;


        public CreateJwtToken()
        {
        }

        public string GenerateToken(JwtDto jwtDto)
        {
            //To be implemented
            return "";
        }
    }
}
