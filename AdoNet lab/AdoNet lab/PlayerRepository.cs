using System.Data.SqlClient;
using AdoNet_lab.Model;

namespace AdoNet_Lab
{
    public static class PlayerRepository
    {
        public static void CreatePlayersTable(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "CREATE TABLE Player (ID INT PRIMARY KEY IDENTITY, FirstName NVARCHAR(100) NOT NULL, " +
                "SecondName NVARCHAR(100) NOT NULL, AddressID INT, TeamID INT, NumberOfGoals INT) ";
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public static void CreateAddressTable(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "CREATE TABLE Address (ID INT PRIMARY KEY IDENTITY, Country NVARCHAR(100) NOT NULL, " +
                "City NVARCHAR(100) NOT NULL, Street NVARCHAR(100) NOT NULL, Building INT, Appartment INT) ";
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public static void AddPlayer(SqlConnection connection, string firstName, string secondName, int teamID,
            Address address)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = string.Format(
                    "INSERT INTO Address (Country, City, Street, Building, Appartment)" +
                    " VALUES ('{0}', '{1}', '{2}', {3}, {4})", address.Country, address.City, address.Street,
                    address.Building, address.Appartment);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT TOP 1 (ID) FROM Address ORDER BY ID DESC";
                var result = command.ExecuteScalar().ToString();
                var addressID = int.Parse(result);
                command.CommandText = "INSERT INTO Player (FirstName, SecondName, AddressID, TeamID, NumberOfGoals)" +
                                      $" VALUES ('{firstName}', '{secondName}', {addressID}, {teamID}, 0)";
                command.ExecuteNonQuery();

                command.CommandText =
                    $"UPDATE Stats SET NumberOfPlayers=NumberOfPlayers+1 WHERE TeamID={teamID}";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Player added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public static void ScoreGoal(SqlConnection connection, int playerID)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = $"UPDATE Player SET NumberOfGoals=NumberOfGoals+1 WHERE ID={playerID}";
                command.ExecuteNonQuery();
                
                command.CommandText = $"SELECT TeamID FROM Player WHERE ID={playerID}";
                var result = command.ExecuteScalar().ToString();
                var teamID = int.Parse(result);
                command.CommandText = $"SELECT ROUND(AVG(NumberOfGoals), 1) FROM Player WHERE TeamID={teamID}";
                int avgGoals = int.Parse(command.ExecuteScalar().ToString());
                command.CommandText = $"UPDATE Stats SET AverageGoalsPerPlayer={avgGoals} WHERE TeamID={teamID}";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Goal score added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public static void ChangeTeam(SqlConnection connection, int playerID, int newTeamID)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = $"SELECT TeamID FROM Player WHERE ID={playerID}";
                int oldTeamID = int.Parse((command.ExecuteScalar().ToString()));
                command.CommandText = $"UPDATE Player SET TeamID={newTeamID} WHERE ID={playerID}";
                command.ExecuteNonQuery();
                
                command.CommandText =
                    $"UPDATE Stats SET NumberOfPlyers=NumberOfPlayers-1 WHERE TeamID={oldTeamID}";
                command.ExecuteNonQuery();
                
                command.CommandText =
                    $"UPDATE Stats SET NumberOfPlyers=NumberOfPlayers+1 WHERE TeamID={newTeamID}";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Team changed successfully");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public static void DeletePlayer(SqlConnection connection, int playerID)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = $"SELECT TeamID FROM Player WHERE ID={playerID}";
                var result = command.ExecuteScalar().ToString();
                var teamID = int.Parse(result);
                command.CommandText = $"DELETE FROM Player WHERE ID={playerID}";
                command.ExecuteNonQuery();
                
                command.CommandText =
                    $"UPDATE Stats SET NumberOfPlyers=NumberOfPlayers-1 WHERE TeamID={teamID}";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Player deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }
    }
}