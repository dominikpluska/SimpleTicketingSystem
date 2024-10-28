using AuthAPI.Data;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthRepository AuthRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            AuthRepository = new AuthRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
