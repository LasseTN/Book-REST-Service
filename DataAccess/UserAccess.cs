using Dapper;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<bool> Create(User entity) {
            bool userInserted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                await conn.OpenAsync();
                var sql = @"
                            INSERT INTO User

                                (userId,
                                firstName,
                                lastName,
                                birthdate,
                                phone,
                                email,
                                address,
                                zipcode,
                                city)
                            VALUES
                                (@userId,
                                @firstName,
                                @lastName,
                                @birthdate,
                                @phone,
                                @email,
                                @address,
                                @zipcode,
                                @city";

                try {
                    var rowsAffected = conn.Execute(sql, entity);
                    if (rowsAffected > 0) {
                        userInserted = true;
                        _logger?.LogInformation($"A user is created with userId {entity.UserId}");

                    }
                } catch (Exception ex) {
                    userInserted = false;
                    _logger?.LogError(ex.Message + "// Create user failed");
                }

            }
            return userInserted;
        }
    
    public Task<User> Get(string id) {
        throw new NotImplementedException();
    }

    public Task<bool> Update(User entity) {
        throw new NotImplementedException();
    }
}
}
