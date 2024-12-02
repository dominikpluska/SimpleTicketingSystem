using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.StaticData
{
    public static class StaticData
    {
        public enum StandardRoles
        {
            Agent, 
            User,
            Administrator
        }

        public enum TicketTypes
        {
            Incident,
            Request
        }

    }
}
