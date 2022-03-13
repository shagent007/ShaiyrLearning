using Microsoft.Data.SqlClient;

namespace Eventator.DataContext.MsSqlServer
{
    public class MsSqlDataBaseExecutor
    {
        private readonly string _connectionString;

        public MsSqlDataBaseExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TResult Execute<TResult>(Func<SqlConnection, TResult> function)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return function(connection);
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

        public TResult ReadItem<TResult>(string sqlCommand, Func<SqlDataReader, TResult> function)
        {
            return Execute<TResult>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return default(TResult);
                reader.Read();
                return function(reader);
            });
        }

        public List<TResult> ReadList<TResult>(string sqlCommand, Func<SqlDataReader, TResult> factory)
        {
            return Execute<List<TResult>>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                List<TResult> _list = new List<TResult>();
                if (!reader.HasRows) return _list;

                while (reader.Read())
                {
                    _list.Add(factory(reader));
                }
              
                return _list;
            });
        }

        public void Write(string sqlCommand)
        {
           Execute(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
            });
        }
    }
}