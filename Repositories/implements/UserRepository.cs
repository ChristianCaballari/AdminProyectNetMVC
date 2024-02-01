using AdminProyectos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdminProyectos.services.implements
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateUser(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var userId = await connection.QuerySingleAsync<int>($@"SP_Create_User",
                new { user.Email, user.NormalizedEmail, user.PasswordHash },
                commandType: System.Data.CommandType.StoredProcedure);
            return userId;
        }

        public async Task<User> SearchByEmail(string normalizedEmail)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<User>($@"SP_SL_Search_User_By_Email",
                new { normalizedEmail },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        private SqlConnection GetSqlConnection()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}
