using System.Data.Odbc;

namespace BD3Trab4.DAOs
{
    public abstract class Dao
    {
        private readonly string _connectionString = $"Dsn={"ORACLE_DSN"};Uid={"robert"};Pwd={"senha1234"}";
        private OdbcConnection _connection;

        protected void OpenConection()
        {
            _connection = new OdbcConnection(_connectionString);
            _connection.Open();
        }

        protected void CloseConnection()
        {
            _connection.Close();
        }

        protected OdbcCommand CreateCommand(string query)
        {
            return new OdbcCommand(query, _connection);
        }
    }
}