using Microsoft.Data.SqlClient;

namespace Eventator.DataContext
{
    public class DataBaseExecutor
    {
        private readonly string _connectionString;

        public DataBaseExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TResult Execute<TResult>(Func<SqlConnection, TResult> function)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                TResult result = function(connection);
                return result;
            }
        }

        public void Execute(Action<SqlConnection> function)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                function(connection);
            }
        }
    }
}

