using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    public class LogControl : ILogControl {
        private readonly ILogAccess _logAccess;

        public LogControl(ILogAccess logAccess) {
            _logAccess = logAccess;
        }

        public async Task<int> Create(Log entity) {
            int insertedId = -1;
            insertedId = await _logAccess.Create(entity);
            return insertedId;
        }

        public Task<List<Log>> GetAllLogs(string userId) {
            throw new NotImplementedException();
        }

        public async Task<Log> GetLogById(int logId) {
            Log foundLog;
            foundLog = await _logAccess.GetLogById(logId);
            return foundLog;
        }

        public Task<List<Log>> GetLogsByUserId(string userId) {
            throw new NotImplementedException();
        }
    }
}
