using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security;
using System.Security.Cryptography;

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
                con.Close();

                if (result > 0)
                {
                    Console.WriteLine("Successful login!");
                    Console.WriteLine("[1] Vy för att bjuda in till spel");
                    Console.WriteLine("[2] Vy för mottagna inbjudningar");
                    Console.WriteLine("[3] Vy för att visa chat");
                    Console.WriteLine("[4] Spela Sten Sax Påse");

                    string input = Console.ReadLine();
                    byte val = Convert.ToByte(input);
                    
                    switch (val)
                    {
                        case 0:
                            break;
                        case 1:
                            Invite();
                            break;
                        case 2:
                            Invites();
                            break;
                        case 3:
                            Chat();
                            break;
                        case 4:
                            PlayGame();
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
                con.Open();
                Console.Clear();
                Console.WriteLine("Sten Sax Påse");
                Console.WriteLine("-------------");

                string player1 = "";
                string player2 = "";
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
                con.Close();
            }

            void Invites()
            {
                con.Open();
                Console.Clear();
                using (SqlCommand command = new SqlCommand("SELECT * FROM [PlayerInvite]", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine($"{reader.GetName(i)}: {reader.GetValue(i)}");
                            }
                            Console.WriteLine();
                        }
                        con.Close();
                    }
                }
            }

            void Chat()
            {
                con.Open();
                Console.Clear();
                using (SqlCommand command = new SqlCommand("SELECT * FROM [Message]", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine($"{reader.GetName(i)}: {reader.GetValue(i)}");
                            }
                            Console.WriteLine();
                            
                        }
                        con.Close();
                    }
                }
            }

            void Invite()
            {
                con.Open();
                Console.Clear();
                using (SqlCommand command = new SqlCommand("INSERT INTO [PlayerInvite] (InviteToUserID, InviteFromUserID) VALUES (@InviteToUserID, @InviteFromUserID)", con))
                {
                    Random rnd = new Random();
                    command.Parameters.AddWithValue("@InviteToUserID", rnd.Next());
                    command.Parameters.AddWithValue("@InviteFromUserID", rnd.Next());

                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Kunde ej skapa en inbjudan!");
                    }
                    else
                    {
                        Console.WriteLine("Skapade en unik inbjudan!");
                    }
                    con.Close();
                }
            }
        }
    }
}