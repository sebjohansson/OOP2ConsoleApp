namespace OOP2ConsoleApp;

public class Game
{
    public static void PlayGame()
            {
                ConnectionString.con.Open();
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

                Console.WriteLine("Spelare 2 valde: " + player2);

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
                ConnectionString.con.Close();
            }
}