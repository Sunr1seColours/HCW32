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
                int needAddNewData = ConsoleOperations.DynamicMenu(ConsoleOperations.addExitMenu);
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
                    userChoice = ConsoleOperations.DynamicMenu(ConsoleOperations.mainMenu);
                    List<Movie> updatedMovies = Catcher.MainAction(movies, autoSaver, userChoice);
                } while (userChoice == 1);
                exitPoint = ConsoleOperations.DynamicMenu(ConsoleOperations.rerunMenu);
            }

            Movie.MovieInfoUpdated -= autoSaver.OnDataUpdate;
            autoSaver.GetNewUpdate -= writer.OnGetNewUpdate;
        } while (exitPoint == 1);
        
        // /Users/zemld/Учеба/Шарпы/КДЗ32/9V.json"
    }
}