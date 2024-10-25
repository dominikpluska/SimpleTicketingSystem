using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter =null, string? includeProperties = null);
        Task<T> Get(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entitties);
    }
}
