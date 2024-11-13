using GlobalServices;

namespace TicketsAPI.Services
{
    public class Services : GlobalServices.GlobalServices
    {
        public Services(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {

        }
    }
}
