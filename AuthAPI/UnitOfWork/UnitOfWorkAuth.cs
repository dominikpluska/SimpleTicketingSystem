using AuthAPI.Data;
using AuthAPI.UnitOfWork.Interfaces;
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
    public class UnitOfWorkAuth : IUnitOfWorkAuth
    {
        public IAuthRepository AuthRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkAuth(ApplicationDbContext context)
        {
            _context = context;
            AuthRepository = new AuthRepository(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
