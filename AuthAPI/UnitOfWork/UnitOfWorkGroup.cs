using AuthAPI.Data;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.UnitOfWork
{
    public class UnitOfWorkGroup : IUnitOfWorkGroup
    {
        public IGroupRepository GroupRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkGroup(ApplicationDbContext context)
        {
            _context = context;
            GroupRepository = new GroupRepository(_context);
        }

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
