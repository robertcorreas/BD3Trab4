using System;
using System.Data.Odbc;

namespace BD3Trab4.DAOs
{
    public abstract class Dao
    {
        private OdbcConnection _connection;
        private OdbcTransaction _currentTransaction;

        private bool _isUsingTransaction;

        private string ConnectionString => BD3Trab4.ConnectionString.Value;

        protected void OpenConection()
        {
            _connection = new OdbcConnection(ConnectionString);
            _connection.Open();
        }

        public void BeginTransaction()
        {
            _isUsingTransaction = true;
            _currentTransaction = _connection.BeginTransaction();
        }

        protected void Commit()
        {
            _currentTransaction.Commit();
        }

        protected void Rollback()
        {
            _currentTransaction.Rollback();
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
            _isUsingTransaction = false;
        }

        protected OdbcCommand CreateCommand(string query)
        {
            if (_isUsingTransaction)
            {
                return new OdbcCommand(query, _connection, _currentTransaction);
            }
            return new OdbcCommand(query, _connection);
        }
    }
}