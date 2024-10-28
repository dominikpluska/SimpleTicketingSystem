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
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly DbContext _context;
        internal DbSet<Group> _dbSet;

        public GroupRepository(DbContext context) : base(context)
        {
            _context = context;
            this._dbSet = _context.Set<Group>();
        }

        public async Task<int> GetGroupId(string groupName)
        {
            return await _dbSet.Where(x => x.GroupName == groupName).Select(x => x.GroupId).FirstOrDefaultAsync();
        }
    }
}
