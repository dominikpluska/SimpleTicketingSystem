﻿using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger.Repository
{
    public interface IUnitOfWork
    {
        public ILogRepository LogRepository { get; }
    }
}
