using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using SimpleLogger.Data;
using SimpleLogger.Interface;
using SimpleLogger.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class SimpleDataBaseLogger : ISimpleLogger
    {
        private IUnitOfWork _unitOfWork;
        private ApplicationDbContext applicationDbContext;
        public SimpleDataBaseLogger()
        {
            _unitOfWork = new UnitOfWork(applicationDbContext);
        }

        public void Log(string message)
        {
            throw new NotImplementedException();
        }
    }
}
