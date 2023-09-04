using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
    public class UserAccess : IUserAccess {

        private readonly string? _connectionString;
        private readonly ILogger<UserAccess> _logger;

        public UserAccess(IConfiguration connectionString, ILogger<UserAccess>? logger = null) {
            _connectionString = connectionString.GetConnectionString("DbAccessConnection");
            _logger = logger;
        }

        public Task<bool> Create(User entity) {
            throw new NotImplementedException();
        }

        public Task<User> Get(string id) {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity) {
            throw new NotImplementedException();
        }
    }
}
