using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
            const string sql = """SELECT * FROM [User] WHERE userId = @id""";
            User result = await _connection.QuerySingleOrDefaultAsync<User>(sql, new {id = userId});
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
            const string sql = """DELETE FROM [User] WHERE userId = @id""";
            await _connection.ExecuteAsync(sql, new { id = userId });
        }
        /// <summary>
        /// This Function is used to Update a user
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="userId"></param>
        /// <returns>Returns Updated User</returns>
        public async Task<User> UpdateUser(NewUserDto newUser, int userId)
        {
            const string sql = """UPDATE [User] SET fullName = @fullName, email = @email, role = @role WHERE userId = @id""";
            await _connection.ExecuteAsync(sql,new { newUser , id = userId});
            return await GetUser(userId);
        }

        // Common functions 
        public async Task GenerateCredentialsForUser(int userId)
        {
            const string sql = """INSERT INTO [User_Credentials] (id) VALUES (@id)""";
            await _connection.ExecuteAsync(sql, new { id = userId });
        }

    }
}
