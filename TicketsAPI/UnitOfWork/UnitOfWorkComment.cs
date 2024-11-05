using DataAccess.Models;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;

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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
