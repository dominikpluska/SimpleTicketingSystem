using AuthAPI.Data;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.UnitOfWork
{
    public class UnitOfWorkRole : IUnitOfWorkRole
    {
        public IRoleRepository RoleRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkRole(ApplicationDbContext context)
        {
            _context = context;
            RoleRepository = new RoleRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
