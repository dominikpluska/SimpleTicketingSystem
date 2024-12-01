using DataAccess.Models;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;
using TicketsAPI.UnitOfWork.Interface;

namespace TicketsAPI.UnitOfWork
{
    public class UnitOfWorkComment : IUnitOfWorkComment
    {
        public ICommentRepository CommentRepository {  get; private set; }
        private readonly DbContext _context;

        public UnitOfWorkComment(ApplicationDbContext context)
        {
            _context = context;
            CommentRepository = new CommentRepository(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
