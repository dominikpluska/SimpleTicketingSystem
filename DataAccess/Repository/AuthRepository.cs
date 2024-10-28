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
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        private readonly DbContext _context;

        public AuthRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
