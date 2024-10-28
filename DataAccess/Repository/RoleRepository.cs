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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DbContext _context;
        internal DbSet<Role> _dbSet;

        public RoleRepository(DbContext context) : base(context)
        {
            _context = context;
            this._dbSet = _context.Set<Role>();
        }

        public async Task<int> GetRoleId(string roleName)
        {
            return await _dbSet.Where(x => x.RoleName == roleName).Select(x => x.RoleId).FirstOrDefaultAsync();

        }
    }
}
