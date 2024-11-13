using DataAccess.Repository.Interface;

namespace AuthAPI.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkJwt
    {
        public IJwtRepository JwtRepository { get; }

        public Task SaveChanges();
    }
}
