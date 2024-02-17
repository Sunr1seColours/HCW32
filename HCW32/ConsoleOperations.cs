using MoviesLib;

namespace HCW32;

/// <summary>
/// Static class which releases all actions with user.
/// </summary>
public static class ConsoleOperations
{
    /// <summary>
    /// Highlights string. The text black and the background is white.
    /// </summary>
    /// <param name="stringToHighlight">String to highlight.</param>
    private static void Highlight(string stringToHighlight)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine(stringToHighlight);
        Console.ResetColor();
    }

    /// <summary>
    /// Asks user to tap a key.
    /// </summary>
    /// <returns>Key that was tapped.</returns>
    private static ConsoleKey GetUserChoice()
    {
        Console.WriteLine("To choose another option, use up and down arrows.");
        Console.WriteLine("And to choose current option, tap 'Enter'.");
        ConsoleKey choice = Console.ReadKey().Key;
        Console.WriteLine();

        return choice;
    }

    /// <summary>
    /// Changes chosen point of menu.
    /// </summary>
    /// <param name="optionsCount">Total amount of options for choose.</param>
    /// <param name="currentOption">Option which is now chosen.</param>
    /// <param name="action">ConsoleKey that user tapped.</param>
    /// <returns>New number of chosen option.</returns>
    private static int ChooseAnotherOption(int optionsCount, int currentOption, ConsoleKey action)
    {
        if (action == ConsoleKey.UpArrow)
        {
            if (currentOption == 1)
            {
                return optionsCount;
            }
            else
            {
                return currentOption - 1;
            }
        }
        else if (action == ConsoleKey.DownArrow)
        {
            if (currentOption == optionsCount)
            {
                return 1;
            }
            else
            {
                return currentOption + 1;
            }
        }

        return currentOption;
    }

    /// <summary>
    /// Main menu options.
    /// </summary>
    public static readonly string[] MainMenu =
    {
        "1. Enter path to file for reading or writing.",
        "2. Sort data by one parameter.",
        "3. Choose movie and change one parameter of it."
    };

    /// <summary>
    /// Sorting menu options.
    /// </summary>
    public static readonly string[] SortOrderMenu =
    {
        "1. Alphabetical order / from small to big.",
        "2. Reversed alphabetical order / from big to small."
    };

    /// <summary>
    /// Menu with two options: the first for add some objects to collection, the second exits the program.
    /// </summary>
    public static readonly string[] AddExitMenu =
    {
        "1. Add data.",
        "2. Exit program."
    };

    /// <summary>
    /// Movie object attributes menu.
    /// </summary>
    public static readonly string[] ChooseParameterMenu =
    {
        "1. Movie Title.",
        "2. Earnings.",
        "3. Actors Percent.",
        "4. Release Year.",
        "5. Genre.",
        "6. Rating."
    };

    /// <summary>
    /// Menu for asking to rerun program.
    /// </summary>
    public static readonly string[] RerunMenu =
    {
        "1. Rerun program.",
        "2. Exit."
    };

    /// <summary>
    /// Asks user to enter a path to file.
    /// </summary>
    /// <returns>String - path to file.</returns>
    public static string? GetPathToFile()
    {
        Console.Write($"Enter a path to file for reading: ");
        return Console.ReadLine();
    }

    /// <summary>
    /// Creates string array with the same length which array of Movie object has.
    /// </summary>
    /// <param name="movies">Array of Movie objects.</param>
    /// <returns>Array of string for menu for choosing one Movie.</returns>
    public static string[] ChooseMovieMenu(Movie[] movies)
    {
        List<string> moviesMenu = new List<string>();
        for (int i = 1; i <= movies.Length; i++)
        {
            moviesMenu.Add($"{i}. Movie №{i}");
        }

        return moviesMenu.ToArray();
    }

    /// <summary>
    /// Releases menu logic.
    /// </summary>
    /// <param name="options">Array of strings. This array contains options to choose.</param>
    /// <param name="currentOption">Already chosen option.</param>
    /// <returns>Finally number of chosen option.</returns>
    public static int DynamicMenu(string[] options, int currentOption = 1)
    {
        do
        {
            Console.Clear();
            for (int i = 1; i <= options.Length; i++)
            {
                if (i == currentOption)
                {
                    Highlight(options[i - 1]);
                }
                else
                {
                    Console.WriteLine(options[i - 1]);
                }
            }

            ConsoleKey choice = GetUserChoice();
            if (choice == ConsoleKey.Enter)
            {
                return currentOption;
            }

            currentOption = ChooseAnotherOption(options.Length, currentOption, choice);
        } while (true);
    }

    /// <summary>
    /// Asks user to enter a new value.
    /// </summary>
    /// <param name="parameterName">Name of parameter.</param>
    /// <returns>String which user entered.</returns>
    public static string? GetNewParameterValue(string parameterName)
    {
        Console.Write($"Enter a new value of {parameterName}: ");
        return Console.ReadLine();
    }

    /// <summary>
    /// Wait for user's action.
    /// </summary>
    public static void Waiter()
    {
        Console.WriteLine("To continue, tap something: ");
        Console.ReadKey();
    }
}