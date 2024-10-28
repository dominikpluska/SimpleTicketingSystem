using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IGroupRepository : IRepository<Group>
    {
        public Task<int> GetGroupId(string groupName);
    }
}
