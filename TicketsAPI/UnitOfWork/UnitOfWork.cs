using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;

namespace TicketsAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITicketRepository TicketRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            TicketRepository = new TicketRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
