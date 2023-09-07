using Dapper;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace DataAccess {
    public class UserAccess : IUserAccess {

        private readonly string? _connectionString;
        private readonly ILogger<UserAccess>? _logger;

        public UserAccess(IConfiguration configuration, ILogger<UserAccess>? logger = null) {
            _connectionString = configuration.GetConnectionString("DbAccessConnection");
            _logger = logger;
        }

        public UserAccess(string connectionString) {
            _connectionString = connectionString;
        }

        public async Task<bool> Create(User entity) {
            bool userInserted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                await conn.OpenAsync();
                var sql = @"
                            INSERT INTO [User] (
                                userId,
                                firstName,
                                lastName,
                                birthdate,
                                phone,
                                email,
                                address,
                                zipcode,
                                city
                            )
                            VALUES (
                                @userId,
                                @firstName,
                                @lastName,
                                @birthdate,
                                @phone,
                                @email,
                                @address,
                                @zipcode,
                                @city
                            )";

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
    
    public async Task<User> Get(string id) {
            User? foundUser = null;

            string sql = @"SELECT
                            userid,
                            firstname,
                            lastname,
                            birthdate,
                            phone,
                            email,
                            address,
                            zipcode,
                            city
                        FROM
                            [User]
                        WHERE
                            userId = @userId";

            using (SqlConnection con = new SqlConnection(_connectionString)) {
                await con.OpenAsync();

                try {
                    foundUser = con.Query<User>(sql, new { userId = id }).FirstOrDefault();
                    if (foundUser != null) {
                        _logger?.LogInformation($"A user was found with userId {foundUser.UserId}");
                    }
                } catch (Exception ex) {
                    _logger?.LogError(ex.Message + "// Get a user failed");
                }
            }
            return foundUser;
    }

    public Task<bool> Update(User entity) {
        throw new NotImplementedException();
    }
}
}
