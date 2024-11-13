using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkAuth
    {
        public IAuthRepository AuthRepository { get; }

        public Task SaveChanges();
    }
}
