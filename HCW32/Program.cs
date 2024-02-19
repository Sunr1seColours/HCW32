using FileOperationsLib;
using MoviesLib;

namespace HCW32;

class Program
{
    static void Main(string[] args)
    {
        int exitPoint;
        do
        {
            AutoSaver autoSaver = new AutoSaver();
            Writer writer = new Writer();
            Movie.MovieInfoUpdated += autoSaver.OnDataUpdate;
            autoSaver.GetNewUpdate += writer.OnGetNewUpdate;

            autoSaver.Updates = new List<DataUpdateEventArgs>();
            exitPoint = 2;

            Catcher.GoToFile();
            List<Movie> movies = Catcher.GetMoviesInfoFromFile(autoSaver);
            while (movies == null)
            {
                int needAddNewData = ConsoleOperations.DynamicMenu(ConsoleOperations.AddExitMenu);
                if (needAddNewData == 2)
                {
                    break;
                }

                Catcher.GoToFile();
                movies = Catcher.GetMoviesInfoFromFile(autoSaver);
            }

            if (movies != null)
            {
                int userChoice;
                do
                {
                    userChoice = ConsoleOperations.DynamicMenu(ConsoleOperations.MainMenu);
                    List<Movie> updatedMovies = Catcher.MainAction(movies, autoSaver, userChoice);
                } while (userChoice == 1);

                exitPoint = ConsoleOperations.DynamicMenu(ConsoleOperations.RerunMenu);
            }

            Movie.MovieInfoUpdated -= autoSaver.OnDataUpdate;
            autoSaver.GetNewUpdate -= writer.OnGetNewUpdate;
        } while (exitPoint == 1);
    }
}