using DataAccess.Repository.Interface;

namespace TicketsAPI.UnitOfWork
{
    public interface IUnitOfWorkComment 
    {
        public ICommentRepository CommentRepository { get; }

        public Task SaveChanges();
    }
}
