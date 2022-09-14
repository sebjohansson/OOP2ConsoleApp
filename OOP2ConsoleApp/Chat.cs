using System.Data.SqlClient;

namespace OOP2ConsoleApp;

public class Chat
{
    public static void CheckMessage()
    {
        ConnectionString.con.Open();
        Console.Clear();
        Console.WriteLine("Välj ID på användaren du vill chatta med: ");
        using (SqlCommand selectUser = new SqlCommand("SELECT UserID, UserName FROM [User]", ConnectionString.con))
        {
            using (SqlDataReader reader = selectUser.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetValue(0)}. {reader.GetValue(1)}");
                }
            }

            var player2 = Console.ReadLine();
            Console.Clear();
            bool validUserId = false;
            using (SqlCommand commandVsUser =
                   new SqlCommand("SELECT UserID, UserName FROM [User] WHERE UserID = " + player2,
                       ConnectionString.con))
            {
                using (SqlDataReader reader = commandVsUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Du chattar med: {reader.GetValue(1)}.");
                        validUserId = true;
                    }
                }
            }

            if (validUserId)
            {
                Console.WriteLine("[1]. Skriv med användaren.");
                Console.WriteLine("[2]. Visa tidigare meddelanden.");
                var input = Console.ReadLine();
                int val;
                while (!int.TryParse(input, out val))
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltit val!");
                    Console.WriteLine("[1]. Skriv med användaren.");
                    Console.WriteLine("[2]. Visa tidigare meddelanden.");
                    input = Console.ReadLine();
                }
                switch (val)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Lämna med att skriva !exit");
                        while (true)
                        {
                            using (SqlCommand command =
                                   new SqlCommand(
                                       "INSERT INTO [Message] (FromUserID, ToUserID, Value, TimeSent, TimeRecieved) VALUES (@FromUserID, @ToUserID, @Value, @TimeSent, @TimeRecieved)",
                                       ConnectionString.con))
                            {
                                var message = Console.ReadLine();
                                command.Parameters.AddWithValue("@ToUserID", long.Parse(player2));
                                command.Parameters.AddWithValue("@FromUserID", Program.loginUserID);
                                command.Parameters.AddWithValue("@Value", message);
                                command.Parameters.AddWithValue("@TimeSent", DateTime.UtcNow);
                                command.Parameters.AddWithValue("@TimeRecieved", DateTime.UtcNow);
                                command.ExecuteNonQuery();
                                if (message == "!exit")
                                {
                                    break;
                                }

                                Console.Clear();
                            }
                        }
                        ConnectionString.con.Close();
                        break;
                    case 2:
                        Console.Clear();
                        using (SqlCommand readMessage =
                               new SqlCommand(
                                   "SELECT FromUserID, ToUserID, Value, TimeSent FROM [Message] WHERE FromUserID = " +
                                   Program.loginUserID, ConnectionString.con))
                        {
                            using (SqlDataReader reader = readMessage.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine(
                                        $"{reader.GetValue(3)} UserID {reader.GetValue(0)}: {reader.GetValue(2)}");
                                }
                            }
                            ConnectionString.con.Close();
                            break;
                        }
                }
            }
        }
    }
}