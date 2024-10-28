using AuthAPI.Data.Dtos;

namespace AuthAPI.JwtGenerator.ICreateJwtToken
{
    public interface ICreateJwtToken
    {
        public string GenerateToken(JwtDto jwtDto);
    }
}
