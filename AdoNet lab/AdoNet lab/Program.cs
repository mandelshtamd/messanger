using System.Configuration;
using System.Data.SqlClient;
using AdoNet_Lab;
using AdoNet_lab.Model;

class Program
{
    public static void Main(string[] args)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            // PlayerRepository.CreatePlayersTable(connection);
            // Console.WriteLine("Таблица Players создана");
            //
            // PlayerRepository.CreateAddressTable(connection);
            // Console.WriteLine("Таблица Address создана");
            //
            // TeamRepository.CreateStatsTable(connection);
            // Console.WriteLine("Таблица Stats создана");
            //
            // TeamRepository.CreateTeamTable(connection);
            // Console.WriteLine("Таблица Team создана");
            //
            // StadiumRepository.CreateStadiumTable(connection);
            // Console.WriteLine("Таблица Stadium создана");
            //
            // TeamRepository.CreateTeamStadiumTable(connection);
            // Console.WriteLine("Таблица TeamStadium создана");
            
            
            var address = new Address("Russia", "Moscow", "Tolstova str.", 16, 15);
            TeamRepository.AddTeam(connection, "Zenit", 1925);
            StadiumRepository.AddStadium(connection, "Zenit Arena");
            TeamRepository.AddStadium(connection, 1, 1);
            PlayerRepository.AddPlayer(connection, "Anton", "Antonov", 1, address);
            TeamRepository.GetTeamStats(connection, 1);
        }
        Console.WriteLine("Подключение закрыто...");
        Console.Read();
    }
}