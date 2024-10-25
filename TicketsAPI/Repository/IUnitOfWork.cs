using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccess.Repository.Interface;

namespace TicketsAPI.Repository
{
    public interface IUnitOfWork
    {
        public ITicketRepository TicketRepository {get;}

        public void SaveChanges();

    }
}
