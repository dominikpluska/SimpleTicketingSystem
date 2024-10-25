using Azure;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TicketsAPI.Services.IServices;

namespace TicketsAPI.Services
{
    public class LogService : ILogService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LogService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async void WriteLog(Log log)
        {
            var jsonLog = JsonSerializer.Serialize(log);
            var content = new StringContent(jsonLog, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Log");

            var response = await client.PostAsync($"/api/log/WriteLog", content);
            var apiContent = await response.Content.ReadAsStringAsync();

        }
    }
}
