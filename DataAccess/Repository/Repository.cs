﻿using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        internal DbSet<T> _dbSet;


        public Repository(DbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entitties)
        {
            _dbSet.RemoveRange(entitties);
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = _dbSet;


            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties!.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties!.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }


        public virtual async Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }


        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
