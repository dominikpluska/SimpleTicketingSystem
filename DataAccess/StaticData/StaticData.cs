using System;
using System.Collections.Generic;
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

        public const string defaultAssingmentGroup = "HelpDesk";

        public static string ReturnConnectionString(string dataBase)
        {
            return $"Server=.;Database={dataBase};Trusted_Connection=True;TrustServerCertificate=True;";
        }



    }
}
