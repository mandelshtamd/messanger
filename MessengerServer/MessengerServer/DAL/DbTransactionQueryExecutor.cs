using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using MessengerServer.DAL.Mappers;

namespace MessengerServer.DAL
{
    public sealed class DbTransactionQueryExecutor : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly TransactionScope _transaction;
        public static DbTransactionQueryExecutor Create(DataStorageSettings settings)
        {
            if (settings == null)
                throw new NullReferenceException(nameof(settings));

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(settings.DbConnectionString))
                {
                    return new DbTransactionQueryExecutor(dbConnection, TransactionScopeOption.Required, System.Transactions.IsolationLevel.Serializable, TransactionManager.DefaultTimeout);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

		internal DbTransactionQueryExecutor(IDbConnection connection)
        {
            if (connection == null)
                throw new NullReferenceException(nameof(connection));

            try
            {
                _connection = connection;
                TransactionOptions transactionOptions = new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.Serializable,
                    Timeout = TransactionManager.DefaultTimeout
                };
                _transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
            }
            catch (Exception ex)
            {
                Dispose();
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private DbTransactionQueryExecutor(IDbConnection connection, TransactionScopeOption transactionScope, System.Transactions.IsolationLevel isolationLevel, TimeSpan transactionTimeout)
        {
            if (connection == null)
                throw new NullReferenceException(nameof(connection));

            try
            {
                _connection = connection;
                TransactionOptions transactionOptions = new TransactionOptions()
                {
                    IsolationLevel = isolationLevel,
                    Timeout = transactionTimeout
                };
                _transaction = new TransactionScope(transactionScope, transactionOptions);
            }
            catch (Exception ex)
            {
                Dispose();
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #region MSSQL

		internal T ExecuteScalar<T>(String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return ExecuteScalar<T>(command, queryString, parameters);
            }
        }

		internal List<T> GetData<T>(IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return GetData<T>(command, mapper, queryString, parameters);
            }
        }

		internal T GetItem<T>(IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return GetItem<T>(command, mapper, queryString, parameters);
            }
        }

		private Int32 ExecuteNonQuery(String queryString, params SqlParameter[] parameters)
        {
            using (IDbCommand command = new SqlCommand(queryString))
            {
                command.Connection = _connection;
                return ExecuteNonQuery(command, queryString, parameters);
            }
        }

        #endregion

		internal Int32 CreateItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);
		internal Int32 DeleteItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);
        public Int32 InsertItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);
		internal Int32 UpdateItem(String queryString, params SqlParameter[] parameters) => ExecuteNonQuery(queryString, parameters);


		private T ExecuteScalar<T>(IDbCommand command, String queryString, params SqlParameter[] parameters)
        {
            T result = default(T);

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                Object resultObj = command.ExecuteScalar();
                if (resultObj != DBNull.Value)
                    result = (T)resultObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return result;
        }


		private List<T> GetData<T>(IDbCommand command, IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            var result = new List<T>();

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(mapper.ReadItem(reader));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

		private T GetItem<T>(IDbCommand command, IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            T result = default(T);

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    result = mapper.ReadItem(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

		private Int32 ExecuteNonQuery(IDbCommand command, String queryString, params SqlParameter[] parameters)
        {
            Int32 rowsAffected;

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return rowsAffected;
        }

		internal void Commit()
        {
            _transaction?.Complete();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                _connection?.Close();

                _transaction?.Dispose();
                _connection?.Dispose();
            }
        }

        ~DbTransactionQueryExecutor()
        {
            Dispose(false);
        }
    }
}
