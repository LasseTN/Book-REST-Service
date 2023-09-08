using DataAccess.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
    public class LogAccess : ILogAccess {
        public Task<int> Create(Log entity) {
            throw new NotImplementedException();
        }

        public Task<List<Log>> GetAllLogs(string userId) {
            throw new NotImplementedException();
        }

        public Task<Log> GetLogById(int logId) {
            throw new NotImplementedException();
        }

        public Task<List<Log>> GetLogsByUserId(string userId) {
            throw new NotImplementedException();
        }
    }
}
