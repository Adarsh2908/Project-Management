using System.Data.SqlClient;

namespace Project_Manager.API.Database
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(string connectionString) {
            _connectionString = connectionString;
        }  

        // Create New Sql Connection 
        public SqlConnection create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
