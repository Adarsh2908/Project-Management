using Dapper;
using Project_Manager.API.Database;
using Project_Manager.API.Models;
using Project_Manager.API.Models.DTO;
using System.Data.SqlClient;

namespace Project_Manager.API.Services
{
    public class UserService(SqlConnectionFactory sqlConnection)
    {
        private SqlConnection _connection = sqlConnection.create();

        /// <summary>
        /// To Get User Details with ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Returns a User</returns>
        public async Task<User> GetUser(int userId)
        {
            const string sql = """SELECT * FROM [User] WHERE userId = @id AND isDeleted = 0;""";
            User result = await _connection.QuerySingleOrDefaultAsync<User>(sql, new { id = userId });
            return result;
        }

        /// <summary>
        /// Creates New User and Returns the Newly Created User.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>Newly Created User</returns>
        public async Task<User> CreateUser(NewUserDto newUser)
        {
            const string sql =
                """
                INSERT INTO [User] (fullName,email,role)
                OUTPUT INSERTED.userId
                VALUES (@fullName,@email,@role)
                """;
            int id = await _connection.ExecuteScalarAsync<int>(sql, newUser);
            // Generate Creds For User 
            await GenerateCredentialsForUser(id);
            return await GetUser(id);
        }
        /// <summary>
        /// Function to Delete a user with Given ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>NULL Result</returns>
        public async Task DeleteUser(int userId)
        {
            const string sql =
                """
                UPDATE [User] SET isDeleted = 1 WHERE userId = @id;
                """;
            await _connection.ExecuteAsync(sql, new { id = userId });
        }
        /// <summary>
        /// This Function is used to Update a user
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="userId"></param>
        /// <returns>Returns Updated User</returns>
        public async Task<User> UpdateUser(User user)
        {
            const string sql =
                """
                UPDATE [User] SET 
                fullName = @fullName, 
                email = @email,
                role = @role 
                WHERE userId = @userId AND isDeleted = 0; 
                """;
            var userData = new { fullName = user.fullName, email = user.email, role = user.role, userId = user.userId };
            await _connection.ExecuteAsync(sql, userData);
            return await GetUser(user.userId);
        }

        /// <summary>
        /// Checks if user is Verified.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean</returns>
        
        public async Task<bool> IsUserVerified(int userId)
        {
            const string sql = 
                """
                SELECT isVerified FROM [User_Credentials] 
                WHERE id = @id;
                """;
            bool isVerified = await _connection.ExecuteScalarAsync<bool>(sql, new { id = userId });
            return isVerified;
        }

        // Common functions 
        public async Task GenerateCredentialsForUser(int userId)
        {
            const string sql = """INSERT INTO [User_Credentials] (id) VALUES (@id)""";
            await _connection.ExecuteAsync(sql, new { id = userId });
        }


    }
}