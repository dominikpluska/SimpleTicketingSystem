using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;

namespace TicketsAPI.UnitOfWork
{
    public class UnitOfWorkTicket : IUnitOfWorkTicket
    {
        public ITicketRepository TicketRepository { get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkTicket(ApplicationDbContext context)
        {
            _context = context;
            TicketRepository = new TicketRepository(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
