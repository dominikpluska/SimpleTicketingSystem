using System.Collections.Generic;

namespace AuthAPI.Data.Dtos
{
    public class JwtDto
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }

    }
}
