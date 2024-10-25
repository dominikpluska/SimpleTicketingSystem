using DataAccess.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        private readonly DbContext _context;

        public LogRepository(DbContext context): base(context) 
        {
            _context = context;
        }

    }
}
