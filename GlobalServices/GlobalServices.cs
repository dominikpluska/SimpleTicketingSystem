using DataAccess.Models;
using GlobalServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GlobalServices
{
    public class GlobalServices : IGlobalServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GlobalServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async void SendEmail(Email email)
        {
            ApiOperation(email, "Mail", $"/api/Mail/WriteLog");
        }

        public async void WriteLog(Log log)
        {
            ApiOperation(log, "Log", $"/api/log/WriteLog");
        }

        private async void ApiOperation(object data, string clientName, string apiPath)
        {
            var jsonLog = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonLog, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient(clientName);

            var response = await client.PostAsync(apiPath, content);
            var apiContent = await response.Content.ReadAsStringAsync();
        }
    }
}
