using System.Data.SqlClient;

namespace AdoNet_Lab
{
    public static class TeamRepository
    {
        public static void CreateTeamStadiumTable(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "CREATE TABLE TeamStadium (TeamID INT, StadiumID INT)";
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public static void CreateTeamTable(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "CREATE TABLE Team (ID INT PRIMARY KEY IDENTITY, TeamName NVARCHAR(100) NOT NULL, EstYear INT) ";
            command.Connection = connection;
            command.ExecuteNonQuery();
        }
        
        public static void CreateStatsTable(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "CREATE TABLE Stats (TeamID INT, NumberOfPlayers INT, " +
                                  "NumberOfStadiums INT, AverageGoalsPerPlayer INT) ";
            command.Connection = connection;
            command.ExecuteNonQuery();
        }
        
        public static void AddTeam(SqlConnection connection, string name, int year)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = $"INSERT INTO Team (TeamName, EstYear) VALUES ('{name}', {year})";
                command.ExecuteNonQuery();
                
                command.CommandText = "SELECT TOP 1 (ID) FROM Team ORDER BY ID DESC";
                var result = command.ExecuteScalar().ToString();
                var id = int.Parse(result);
                command.CommandText =
                    $"INSERT INTO Stats (TeamID, NumberOfPlayers, NumberOfStadiums, AverageGoalsPerPlayer) VALUES ({id}, 0, 0, 0)";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Team added successfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public static void RenameTeam(SqlConnection connection, int id, string newName)
        {
            var expression = string.Format("UPDATE Team SET Name='{1}' WHERE ID={0}", id, newName);
            SqlCommand command = new SqlCommand(expression, connection);
            command.ExecuteNonQuery();
        }

        public static void AddStadium(SqlConnection connection, int teamID, int stadiumID)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = $"INSERT INTO TeamStadium (TeamID, StadiumID) VALUES ({teamID}, {stadiumID})";
                command.ExecuteNonQuery();
                
                command.CommandText = $"UPDATE Stats SET NumberOfStadiums=NumberOfStadiums+1 WHERE TeamID={teamID}";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Stadium added successfully");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public static void RemoveStadium(SqlConnection connection, int teamID, int stadiumID)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = $"DELETE FROM TeamStadium WHERE TeamID={teamID}";
                command.ExecuteNonQuery();
                command.CommandText = $"UPDATE Stats SET NumberOfStadiums=NumberOfStadiums-1 WHERE TeamID={teamID}";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Stadium deleted successfully");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public static void GetTeamStats(SqlConnection connection, int teamID)
        {
            string procedure = "sp_GetTeamStats";
            /*
             * CREATE PROCEDURE [dbo].[sp_GetTeamStats] @teamID int AS SELECT * FROM Stats WHERE TeamID=@teamID
             */
            SqlCommand command = new SqlCommand(procedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter teamIDParam = new SqlParameter
            {
                ParameterName = "@teamID",
                Value = teamID
            };
            command.Parameters.Add(teamIDParam);

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var playersNum = reader.GetInt32(1);
                    var stadiumsNum = reader.GetInt32(2);
                    Console.WriteLine($"Number of players {playersNum}, number of stadiums: {stadiumsNum}");
                }
            }
            reader.Close();
        }
    }
}