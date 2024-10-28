using GlobalServices;

namespace AuthAPI.Services
{
    public class Services : GlobalServices.GlobalServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Services(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
