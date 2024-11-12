using AuthAPI.Data;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.UnitOfWork
{
    public class UnitOfWorkJwt : IUnitOfWorkJwt
    {
        public IJwtRepository JwtRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkJwt(ApplicationDbContext context)
        {
            _context = context;
            JwtRepository = new JwtRepository(_context);
        }

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
