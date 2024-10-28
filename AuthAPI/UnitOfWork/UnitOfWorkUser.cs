using AuthAPI.Data;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.UnitOfWork
{
    public class UnitOfWorkUser : IUnitOfWorkUser
    {
        public IUserRepository UserRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkUser(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
