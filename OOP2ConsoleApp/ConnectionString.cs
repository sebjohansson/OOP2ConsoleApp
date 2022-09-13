using System.Data.SqlClient;

namespace OOP2ConsoleApp;

public class ConnectionString
{
    static string connectionString = @"Server=localhost\SQLEXPRESS01;Database=RockPaperScissor;Trusted_Connection=True";
    public static SqlConnection con = new SqlConnection(connectionString);
}