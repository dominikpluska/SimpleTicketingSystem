using DataAccess.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class JwtRepository : Repository<JWT>, IJwtRepository
    {
        private readonly DbContext _context;
        internal DbSet<JWT> _dbSet;

        public JwtRepository(DbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<JWT>();
        }

    }
}
