using DataAccess.Repository.Interface;

namespace TicketsAPI.UnitOfWork
{
    public interface IUnitOfWorkCategory
    {
        public ICategoryRepository CategoryRepository { get; }

        public Task SaveChanges();
    }
}
