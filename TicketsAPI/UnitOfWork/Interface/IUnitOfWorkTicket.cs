using DataAccess.Models;
using DataAccess.Repository.Interface;

namespace TicketsAPI.UnitOfWork.Interface
{
    public interface IUnitOfWorkTicket 
    {
        public ITicketRepository TicketRepository { get; }

        public Task SaveChanges();
    }
}
