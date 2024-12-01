using DataAccess.Models;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TicketsAPI.Data;
using TicketsAPI.UnitOfWork.Interface;

namespace TicketsAPI.UnitOfWork
{
    public class UnitOfWorkTicket : IUnitOfWorkTicket
    {
        public ITicketRepository TicketRepository { get; private set; }
        private readonly ApplicationDbContext _context;
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
