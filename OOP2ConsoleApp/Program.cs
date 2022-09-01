using System.Data;
using System.Data.SqlClient;

namespace OOP2ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = @"Server=localhost;Database=RockPaperScissor;Trusted_Connection=True";

        using var con = new SqlConnection(connectionString);
        try
        {
            Console.WriteLine("Opening SQL Connection");
            con.Open();
            Console.WriteLine("Connection successful!");
            
            Console.WriteLine("[1] Vy för inloggning av spelare");
            Console.WriteLine("[2] Vy för att bjuda in till spel");
            Console.WriteLine("[3] Vy för mottagna inbjudningar");
            Console.WriteLine("[4]");
            Console.WriteLine("[5]");

            string input = Console.ReadLine();
            byte result = Convert.ToByte(input);
            switch (result)
            {
                case 0:
                    break;
                case 1:
                    LogIn();
                    Console.WriteLine("xD");
                    break;
                case 2:
                    break;
                default:
                    break;
            }

            void LogIn()
            {
                using (SqlCommand cmd = new SqlCommand("EXEC hittaUsers"))
                {
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Detta gick fan inte...");
        }
    }
}