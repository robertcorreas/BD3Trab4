using System;
using System.Data.Odbc;

namespace BD3Trab4.DAOs
{
    public abstract class Dao
    {
        private OdbcConnection _connection;

        private string ConnectionString => BD3Trab4.ConnectionString.Value;

        protected void OpenConection()
        {
            _connection = new OdbcConnection(ConnectionString);
            _connection.Open();
        }

        public bool TestarConexão()
        {
            try
            {
                OpenConection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
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