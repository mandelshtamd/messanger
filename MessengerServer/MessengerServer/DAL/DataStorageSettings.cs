using System;

namespace MessengerServer.DAL
{
    public sealed class DataStorageSettings
    {
        internal String DbConnectionString { get; }

        internal DataStorageSettings(String dbConnectionString)
        {
            DbConnectionString = dbConnectionString;
        }

        public DataStorageSettings()
        {
            DbConnectionString =
                "Data Source=.;Initial Catalog=messenger_db;Integrated Security=True;MultipleActiveResultSets=True;App=Messenger;Connection Timeout=60"
                ;
        }
    }
}