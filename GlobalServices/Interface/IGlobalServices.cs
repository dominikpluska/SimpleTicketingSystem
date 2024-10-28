using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalServices.Interface
{
    public interface IGlobalServices
    {
        void WriteLog(Log log);

        void SendEmail(Email email);

    }
}
