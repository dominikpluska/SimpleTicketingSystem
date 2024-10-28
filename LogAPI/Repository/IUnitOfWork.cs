﻿using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAPI.Repository
{
    public interface IUnitOfWork
    {
        public ILogRepository LogRepository { get; }

        public void SaveChanges();
    }
}