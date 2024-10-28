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

        public RoleRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public int GetRoleId()
        {
            //To be implemented
            return 1;
        }
    }
}
