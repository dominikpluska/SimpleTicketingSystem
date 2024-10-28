using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAuthRepository AuthRepository { get; }

        public void SaveChanges();
    }
}
