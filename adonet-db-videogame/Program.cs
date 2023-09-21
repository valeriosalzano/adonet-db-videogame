using ConsoleTables;
using System.Globalization;

namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userChoice;
            do
            {
                var menu = new ConsoleTable("                   MENU");

                menu.AddRow("1 - Insert a new video game.");
                menu.AddRow("2 - Search for a video game by ID.");
                menu.AddRow("3 - Search for all games with the name containing a certain string.");
                menu.AddRow("4 - Delete a video game.");
                menu.AddRow("0 - Close the program");

                Console.WriteLine("\n");
                menu.Write();
                Console.WriteLine("\n");

                Console.Write("Press the desidered command key: ");
                userChoice = Console.ReadLine() ?? "";

                switch (userChoice)
                {
                    case "1":
                        InsertVideogame();
                        break;
                    case "2":
                        FindVideogameById();
                        break;
                    case "3":
                        FindVideogameByName();
                        break;
                    case "4":
                        DeleteVideogame();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid key.");
                        break;

                }
            } while (userChoice != "0");

            Console.WriteLine("\n --- Program END --- \n");
        }

        public static void InsertVideogame()
        {
            Console.WriteLine("\n--- Inserting a new videogame ---\n");

            Console.Write("Enter the videogame name: ");
            string videogameName = GetValidStringFromUser();

            string videogameOverview = GetOptionalParameterFromUser("overview");

            Console.Write("Enter the videogame release date (dd/mm/yyyy): ");
            DateTime videogameReleaseDate = GetValidDateFromUser();

            Console.Write("Enter the software house id: ");
            int videogameSHId = GetValidPositiveIntegerFromUser();

            bool inserted;
            try
            {
                Videogame newVideogame = new Videogame(0, videogameName, videogameOverview, videogameReleaseDate, videogameSHId);
                inserted = VideogameManager.InsertVideogame(newVideogame);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                inserted = false;
            }
            
            
            Console.WriteLine(inserted ? "Success!" : "Error! Something went wrong.");

        }

        public static void FindVideogameById()
        {

            Console.WriteLine("\n--- Finding a videogame by ID ---\n");

            Console.Write("Enter the videogame ID: ");
            int videogameId = GetValidPositiveIntegerFromUser();

            bool found = VideogameManager.FindVideogameById(videogameId, out Videogame? foundVideogame);

            Console.WriteLine(found ? ("Videogame found! " + foundVideogame) : "Videogame not found.");

        }
        public static void FindVideogameByName()
        {

            Console.WriteLine("\n--- Finding a videogame by Name ---\n");

            Console.Write("Enter the videogame name: ");
            string videogameName = GetValidStringFromUser();

            List<Videogame> foundVideogames = VideogameManager.FindVideogamesByName(videogameName);

            if (foundVideogames.Count() > 0)
                foreach (Videogame videogame in foundVideogames)
                    Console.WriteLine($"- {videogame}");
            else
                Console.WriteLine("Videogame not found.");
        }
        public static void DeleteVideogame()
        {
            Console.WriteLine("\n--- Deleting a videogame by ID ---\n");

            Console.Write("Enter the videogame ID: ");
            int videogameId = GetValidPositiveIntegerFromUser();

            bool found = VideogameManager.DeleteVideogame(videogameId);

            Console.WriteLine(found ? ("Success! Videogame deleted") : "Error! Videogame not found.");

        }
        // USER INPUT FUNCTIONS
        public static string GetValidStringFromUser()
        {
            string? userInput = Console.ReadLine();
            while (string.IsNullOrEmpty(userInput) || userInput.Trim() == "")
            {
                Console.Write("Inserisci un valore valido: ");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
        public static DateTime GetValidDateFromUser()
        {
            DateTime userInput;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out userInput))
            {
                Console.Write("Inserisci un formato di data valido: ");
            }
            return userInput;
        }
        public static int GetValidPositiveIntegerFromUser()
        {
            int userInput;
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput <= 0)
            {
                Console.Write("Inserisci un numero positivo valido: ");
            }
            return userInput;
        }
        public static string GetOptionalParameterFromUser(string parameterName)
        {
            string outputString = "";
            bool looping = true;
            while (looping)
            {
                string userChoice = "";
                Console.Write($"Do you want to set the {parameterName}? (y/n) ");
                userChoice = GetValidStringFromUser().ToLower();

                if (userChoice == "y")
                {
                    Console.Write($"Enter the {parameterName}: ");
                    outputString = GetValidStringFromUser();
                    looping = false;
                }
                else if (userChoice == "n" || userChoice == "")
                {
                    Console.WriteLine("Fine.");
                    looping = false;
                }
                else
                    Console.WriteLine("Invalid key.");
            }
            return outputString;
        }
    }
}
