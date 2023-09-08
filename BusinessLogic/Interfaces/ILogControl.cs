﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces {
    public interface ILogControl {

        Task<int> Create(Log entity);
        Task<List<Log>> GetLogsByUserId(string userId);
        Task<List<Log>> GetAllLogs(string userId);
        Task<Log> GetLogById(int logId);
    }
}
