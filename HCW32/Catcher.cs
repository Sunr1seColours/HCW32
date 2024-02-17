using FileOperationsLib;
using MoviesLib;

namespace HCW32;

/// <summary>
/// Static class which releases all logic and catches exceptions.
/// </summary>
public static class Catcher
{
    /// <summary>
    /// Asks user to get a path to file for reading.
    /// </summary>
    public static void GoToFile()
    {
        do
        {
            try
            {
                Connector.PathToFileToRead = ConsoleOperations.GetPathToFile();
                break;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        } while (true);
    }

    /// <summary>
    /// Reads JSON file and gets information about Movie objects that file contains.
    /// </summary>
    /// <param name="autoSaver">AutoSaver object for looking for updates in collection.</param>
    /// <returns>Collection of Movie objects.</returns>
    public static List<Movie> GetMoviesInfoFromFile(AutoSaver autoSaver)
    {
        List<Movie> movies = null;
        try
        {
            (movies, DataUpdateEventArgs getMoviesInfo) = Reader.ReadFile(Connector.PathToFileToRead);
            Movie.MovieInfoUpdateFire(getMoviesInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return movies;
    }

    /// <summary>
    /// Adds new Movies to existed collection or sorts collection of Movie objects or
    /// changes one pole in one Movie object in collection.
    /// </summary>
    /// <param name="currentMovies">Already existed collection.</param>
    /// <param name="autoSaver">AutoSaver object which is looking for updates in collection.</param>
    /// <param name="optionNumber">Number of chosen action.</param>
    /// <returns>Changed collection of Movie objects.</returns>
    public static List<Movie> MainAction(List<Movie> currentMovies, AutoSaver autoSaver, int optionNumber)
    {
        List<Movie> changedMovies = new List<Movie>();
        changedMovies.AddRange(currentMovies);
        int attributeNumber;
        switch (optionNumber)
        {
            case 1:
                GoToFile();
                changedMovies.AddRange(GetMoviesInfoFromFile(autoSaver));
                Movie.MovieInfoUpdateFire(new DataUpdateEventArgs(changedMovies));
                
                return changedMovies;
            case 2:
                attributeNumber = ConsoleOperations.DynamicMenu(ConsoleOperations.chooseParameterMenu);
                int orderNumber = ConsoleOperations.DynamicMenu(ConsoleOperations.sortOrderMenu);
                changedMovies = Sorter.Sort(changedMovies, (Movie.MovieAttributes)(attributeNumber - 1), orderNumber);
                Movie.MovieInfoUpdateFire(new DataUpdateEventArgs(changedMovies));
                
                return changedMovies;
            case 3:
                int numberOfMovie = ConsoleOperations.DynamicMenu(ConsoleOperations.ChooseMovieMenu(currentMovies.ToArray()));
                attributeNumber = ConsoleOperations.DynamicMenu(ConsoleOperations.chooseParameterMenu);
                do
                {
                    string attributeName = Movie.GetNameOfAttribute((Movie.MovieAttributes)(attributeNumber - 1));
                    try
                    {
                        changedMovies[numberOfMovie - 1].ChangeOneAttribute((Movie.MovieAttributes)(attributeNumber - 1),
                            ConsoleOperations.GetNewParameterValue(attributeName));
                        Movie.MovieInfoUpdateFire(new DataUpdateEventArgs(changedMovies));
                        
                        return changedMovies;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                } while (true);
            default:
                return changedMovies;
        }
    }    
}