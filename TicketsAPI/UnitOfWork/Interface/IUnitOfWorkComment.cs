using DataAccess.Repository.Interface;

namespace TicketsAPI.UnitOfWork.Interface
{
    public interface IUnitOfWorkComment
    {
        public ICommentRepository CommentRepository { get; }

        public Task SaveChanges();
    }
}
