using DataAccess.Repository.Interface;

namespace AuthAPI.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRole
    {
        public IRoleRepository RoleRepository { get; }

        public void SaveChanges();
    }
}
