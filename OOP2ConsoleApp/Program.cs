using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security;
using System.Security.Cryptography;
using static OOP2ConsoleApp.ConnectionString;

namespace OOP2ConsoleApp;

class Program
{
    public static long loginUserID = 0;
    static void Main(string[] args)
    {
        using (con)
        {
            try
            {
                //Login funktion
                while (loginUserID == 0)
                {
                    con.Open();
                    SqlCommand cmd =
                        new SqlCommand("SELECT UserID FROM [User] WHERE UserName=@UserName AND Password=@Password",
                            con);
                    
                    Console.WriteLine("Write your username: ");
                    var loginUsername = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    var loginPassword = Console.ReadLine();

                    cmd.Parameters.AddWithValue("@UserName", loginUsername);
                    cmd.Parameters.AddWithValue("@Password", loginPassword);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loginUserID = reader.GetInt64(0);
                        }
                    }
                    Console.Clear();
                    con.Close();
                }

                if (loginUserID > 0)
                {
                    Console.WriteLine("Successful login!");
                    Console.Clear();
                    Console.WriteLine("[1] Vy för att bjuda in till spel");
                    Console.WriteLine("[2] Vy för mottagna inbjudningar");
                    Console.WriteLine("[3] Vy för att visa chat");
                    Console.WriteLine("[4] Spela Sten Sax Påse");

                    var input = Console.ReadLine();
                    int val;
                    while (!int.TryParse(input, out val))
                    {
                        Console.Clear();
                        Console.WriteLine("Ogiltit val!");
                        Console.WriteLine("[1] Vy för att bjuda in till spel");
                        Console.WriteLine("[2] Vy för mottagna inbjudningar");
                        Console.WriteLine("[3] Vy för att visa chat");
                        Console.WriteLine("[4] Spela Sten Sax Påse");
                        input = Console.ReadLine();
                    }
                    switch (val)
                    {
                        case 0:
                            break;
                        case 1:
                            Invites.InvitePlayer();
                            break;
                        case 2:
                            Invites.InvitesRecieved();
                            break;
                        case 3:
                            Chat.CheckMessage();
                            break;
                        case 4:
                            Game.PlayGame();
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
                Console.WriteLine(e.Message);
                con.Close();
            }
        }
    }
}