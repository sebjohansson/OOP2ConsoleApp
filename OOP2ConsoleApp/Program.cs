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
    private static long loginUserID = 0;
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
                using (SqlCommand commandUser = new SqlCommand("SELECT UserID, UserName FROM [User]", con))

                {
                    using (SqlDataReader reader = commandUser.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetValue(0)}: {reader.GetValue(1)}");
                        }
                    }

                    Console.WriteLine("Skriv spelare 2 id");
                    var player2 = Console.ReadLine();
                    bool validUserId = false;
                    using (SqlCommand commandVsUser =
                           new SqlCommand("SELECT UserID, UserName FROM [User] WHERE UserID = " + player2, con))
                    {
                        using (SqlDataReader reader = commandVsUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Du valde att spela emot {reader.GetValue(1)}");
                                validUserId = true;
                            }
                        }
                    }


                    if (validUserId)
                    {
                        using (SqlCommand command =
                               new SqlCommand(
                                   "INSERT INTO [PlayerInvite] (InviteToUserID, InviteFromUserID) VALUES (@InviteToUserID, @InviteFromUserID)",
                                   con))
                        {
                            command.Parameters.AddWithValue("@InviteToUserID", long.Parse(player2));
                            command.Parameters.AddWithValue("@InviteFromUserID", loginUserID);

                            int result = command.ExecuteNonQuery();
                            if (result < 0)
                            {
                                Console.WriteLine("Kunde ej skapa en inbjudan!");
                            }
                        }
                    }
                    con.Close();
                }
            }
        }
    }
}