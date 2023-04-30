using System.Data;
using System.Data.SqlClient;
using MessengerServer.DAL.Mappers;

namespace MessengerServer.DAL
{
    public class DbQueryExecutor
    {
        private static readonly DataStorageSettings s_settings = new DataStorageSettings(
            "Data Source=.;Initial Catalog=messenger_db;Integrated Security=True;MultipleActiveResultSets=True;App=Messenger;Connection Timeout=60");

        #region GetScalar

        public static T GetScalar<T>(String queryString, params SqlParameter[] parameters)
        {
            using (IDbConnection connection = new SqlConnection(s_settings.DbConnectionString))
            {
                return GetScalar<T>(connection, queryString, parameters);
            }
        }

        private static T GetScalar<T>(IDbConnection connection, String queryString, params SqlParameter[] parameters)
        {
            T result = default(T);

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                IDbCommand command = new SqlCommand(queryString);
                command.Connection = connection;

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

        #endregion

        #region GetData

        public static List<T> GetData<T>(IMapper<T> mapper, String queryString)
        {
            using (IDbConnection connection = new SqlConnection(s_settings.DbConnectionString))
            {
                return GetDataParametrized(connection, mapper, queryString, null);
            }
        }

        public static List<T> GetDataParametrized<T>(IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            using (IDbConnection connection = new SqlConnection(s_settings.DbConnectionString))
            {
                return GetDataParametrized(connection, mapper, queryString, parameters);
            }
        }


        private static List<T> GetDataParametrized<T>(IDbConnection connection, IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            var result = new List<T>();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                IDbCommand command = new SqlCommand(queryString);
                command.Connection = connection;

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
            }

            return result;
        }

        #endregion

        #region GetItem

        public static T GetItem<T>(IMapper<T> mapper, String queryString)
        {
            using (IDbConnection connection = new SqlConnection(s_settings.DbConnectionString))
            {
                return GetItemParametrized(connection, mapper, queryString, null);
            }
        }

        public static T GetItemParametrized<T>(IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            using (IDbConnection connection = new SqlConnection(s_settings.DbConnectionString))
            {
                return GetItemParametrized(connection, mapper, queryString, parameters);
            }
        }

        private static T GetItemParametrized<T>(IDbConnection connection, IMapper<T> mapper, String queryString, params SqlParameter[] parameters)
        {
            T result = default(T);

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                IDbCommand command = new SqlCommand(queryString);
                command.Connection = connection;

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                IDataReader reader = command.ExecuteReader();
                reader.Read();
                result = mapper.ReadItem(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion            

        public static Int32 CreateItemParametrized(String queryString, params SqlParameter[] parameters) => ExecuteNonQueryParametrized(queryString, parameters);
        public static Int32 DeleteItemParametrized(String queryString, params SqlParameter[] parameters) => ExecuteNonQueryParametrized(queryString, parameters);
        public static Int32 InsertItemParametrized(String queryString, params SqlParameter[] parameters) => ExecuteNonQueryParametrized(queryString, parameters);
        public static Int32 UpdateItemParametrized(String queryString, params SqlParameter[] parameters) => ExecuteNonQueryParametrized(queryString, parameters);

        private static Int32 ExecuteNonQueryParametrized(String queryString, params SqlParameter[] parameters)
        {
            using (IDbConnection connection = new SqlConnection(s_settings.DbConnectionString))
            {
                return ExecuteNonQueryParametrized(connection, queryString, parameters);
            }
        }

        private static Int32 ExecuteNonQueryParametrized(IDbConnection connection, String queryString, params SqlParameter[] parameters)
        {
            Int32 rowsAffected = 0;

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                IDbCommand command = new SqlCommand(queryString);
                command.Connection = connection;

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rowsAffected;
        }
    }
}
