using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleLogger.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILogRepository LogRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            LogRepository = new LogRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
