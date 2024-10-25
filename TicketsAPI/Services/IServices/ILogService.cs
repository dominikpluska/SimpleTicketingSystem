using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace TicketsAPI.Services.IServices
{
    public interface ILogService
    {
        void WriteLog(Log log);
    }
}
