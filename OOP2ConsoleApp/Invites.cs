using System.Data.SqlClient;

namespace OOP2ConsoleApp;

public class Invites
{
    public static void InvitePlayer()
    {
        ConnectionString.con.Open();
        Console.Clear();
        using (SqlCommand commandUser = new SqlCommand("SELECT UserID, UserName FROM [User]", ConnectionString.con))

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
                   new SqlCommand("SELECT UserID, UserName FROM [User] WHERE UserID = " + player2, ConnectionString.con))
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
                           ConnectionString.con))
                {
                    command.Parameters.AddWithValue("@InviteToUserID", long.Parse(player2));
                    command.Parameters.AddWithValue("@InviteFromUserID", Program.loginUserID);

                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Kunde ej skapa en inbjudan!");
                    }
                }
            }
            ConnectionString.con.Close();
        }
    }

    public static void InvitesRecieved()
    {
        ConnectionString.con.Open();
        Console.Clear();
        //int i = (int)Program.loginUserID;
        
        using (SqlCommand command = new SqlCommand("SELECT InviteFromUserID, InviteToUserID FROM [PlayerInvite] WHERE InviteFromUserID = " + Program.loginUserID, ConnectionString.con))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"UserID {reader.GetValue(0)} bjöd in: UserID {reader.GetValue(1)}");
                }
            }
        }
    }
}