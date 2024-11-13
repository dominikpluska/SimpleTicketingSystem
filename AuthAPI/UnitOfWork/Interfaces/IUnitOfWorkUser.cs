using DataAccess.Repository.Interface;

namespace AuthAPI.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkUser
    {
        public IUserRepository UserRepository { get; }

        public Task SaveChanges();
    }
}
