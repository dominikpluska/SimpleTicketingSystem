using DataAccess.Repository.Interface;

namespace AuthAPI.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkGroup
    {
        public IGroupRepository GroupRepository { get; }

        public void SaveChanges();
    }
}
