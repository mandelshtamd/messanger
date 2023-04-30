using System.Data.SqlClient;

namespace AdoNet_Lab
{
    public static class StadiumRepository
    {
        public static void CreateStadiumTable(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "CREATE TABLE Stadium (ID INT PRIMARY KEY IDENTITY, StadiumName NVARCHAR(100) NOT NULL)";
            command.Connection = connection;
            command.ExecuteNonQuery();
        }
        
        public static void AddStadium(SqlConnection connection, string name)
        {
            string expression = $"INSERT INTO Stadium (StadiumName) VALUES ('{name}')";
            SqlCommand command = new SqlCommand(expression, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Stadium added successfully");
        }
    }
}