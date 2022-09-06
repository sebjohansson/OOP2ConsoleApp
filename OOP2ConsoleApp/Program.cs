using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OOP2ConsoleApp;

class Program
{
    //ställer in connection string
    static string connectionString = @"Server=localhost\SQLEXPRESS01;Database=RockPaperScissor;Trusted_Connection=True";
    static SqlConnection con = new SqlConnection(connectionString);

    static void Main(string[] args)
    {
        
        using (con)
        {
            try
            {
                //Öppnar connectionen
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE UserName=@UserName AND Password=@Password", con);
                
                //Login
                Console.WriteLine("Write your username: ");
                var loginUsername = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                var loginPassword = Console.ReadLine();
                
                cmd.Parameters.AddWithValue("@UserName", loginUsername);
                cmd.Parameters.AddWithValue("@Password", loginPassword);
                
                int result = (int)cmd.ExecuteScalar();
                Console.Clear();
                //Console.WriteLine(result);

                if (result > 0)
                {
                    Console.WriteLine("Successful login!");
                    Console.WriteLine("[1] Vy för att bjuda in till spel");
                    Console.WriteLine("[2] Vy för mottagna inbjudningar");
                    Console.WriteLine("[3]");
                    Console.WriteLine("[4] Spela Sten Sax Påse");

                    string input = Console.ReadLine();
                    byte val = Convert.ToByte(input);
                    
                    switch (val)
                    {
                        case 0:
                            break;
                        case 1:
                            //LogIn();
                            Console.WriteLine("xD");
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            PlayGame();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect login!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(e.Message);
                con.Close();
            }
            void PlayGame()
            {
                Console.Clear();
                Console.WriteLine("Sten Sax Påse");
                Console.WriteLine("-------------");

                string player1 = "";
                string player2 = "";

                int rounds = 0;
                int bestof = 0;

                rounds++;
                Console.WriteLine("Runda: " + rounds);
                Console.WriteLine();

                while (player1 != "Sten" && player1 != "Påse" && player1 != "Sax")
                {
                    Console.WriteLine("Välj Sten, Sax eller Påse: ");
                    player1 = Console.ReadLine();
                    Console.Clear();
                }

                Console.WriteLine("Spelare 1 valde: " + player1);

                while (player2 != "Sten" && player2 != "Påse" && player2 != "Sax")
                {
                    Console.WriteLine("Välj Sten, Sax eller Påse: ");
                    player2 = Console.ReadLine();
                    Console.Clear();
                }

                Console.WriteLine("Spelare 2 valde: "+ player2);

                Console.WriteLine("Player 1 = " + player1);
                Console.WriteLine("Player 2 = " + player2);
                
                switch (player1)
                {
                    case "Sten":
                        switch (player2)
                        {
                            case "Sten":
                                Console.WriteLine("Lika!");
                                break;
                            case "Påse":
                                Console.WriteLine("Spelare 2 vinner!");
                                break;
                            case "Sax":
                                Console.WriteLine("Spelare 1 vinner!");
                                break;
                        }
                        break;
                    case "Påse":
                        switch (player2)
                        {
                            case "Sten":
                                Console.WriteLine("Spelare 1 vinner!");
                                break;
                            case "Påse":
                                Console.WriteLine("Lika!");
                                break;
                            case "Sax":
                                Console.WriteLine("Spelare 2 vinner!");
                                break;
                        }
                        break;
                    case "Sax":
                        switch (player2)
                        {
                            case "Sten":
                                Console.WriteLine("Spelare 2 vinner!");
                                break;
                            case "Påse":
                                Console.WriteLine("Spelare 1 vinner!");
                                break;
                            case "Sax":
                                Console.WriteLine("Lika!");
                                break;
                        }
                        break;
                }
            }
        }
    }
}