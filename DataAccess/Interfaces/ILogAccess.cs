using Model;

namespace DataAccess.Interfaces {
    public interface ILogAccess {
        Task<int> Create(Log entity);
        Task<List<Log>> GetLogsByUserId(string userId);
        Task<List<Log>> GetAllLogs(string userId);
        Task<Log> GetLogById(int logId);
    }
}
