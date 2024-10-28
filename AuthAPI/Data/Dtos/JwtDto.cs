using System.Collections.Generic;

namespace AuthAPI.Data.Dtos
{
    public class JwtDto
    {
        public string Username { get; set; }

        public List<string> Roles { get; set; }

        public string TokenString { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
