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
    public class AuthRepository : Repository<UserAccount>, IAuthRepository
    {
        private readonly DbContext _context;
        internal DbSet<UserAccount> _dbSet;

        public AuthRepository(DbContext context) : base(context)
        {
            _context = context;
            this._dbSet = _context.Set<UserAccount>();
        }

        public bool CheckIfUserExists(string userAccount)
        {
            if(!_dbSet.Any(x => x.UserName == userAccount))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
