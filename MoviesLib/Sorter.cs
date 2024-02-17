namespace MoviesLib;

/// <summary>
/// Static class which represents methods for sorting collection of Movie objects. 
/// </summary>
public static class Sorter
{
    /// <summary>
    /// Gets string values of parameters to sort by.
    /// </summary>
    /// <param name="movies">Collection of Movie objects to sort.</param>
    /// <param name="attribute">Value of MovieAttribute. Shows by what types of attribute sorting will be.</param>
    /// <returns>Array of strings - values of each Movie's attribute.</returns>
    private static string[] GetStringMoviesAttributes(List<Movie> movies, Movie.MovieAttributes attribute)
    {
        string[] moviesAttributes = new string[movies.Count];
        for (int i = 0; i < movies.Count; i++)
        {
            switch (attribute)
            {
                case Movie.MovieAttributes.Title:
                    moviesAttributes[i] = movies[i].Title;
                    break;
                case Movie.MovieAttributes.Genre:
                    moviesAttributes[i] = movies[i].Genre;
                    break;
            }
        }

        return moviesAttributes;
    }

    /// <summary>
    /// Gets double values of parameters to sort by.
    /// </summary>
    /// <param name="movies">List of Movie objects to sort.</param>
    /// <param name="attribute">Value of MovieAttribute. Shows by what types of attribute sorting will be.</param>
    /// <returns>Array of double numbers - values of each Movie's attribute.</returns>
    private static double[] GetDigitalMoviesAttributes(List<Movie> movies, Movie.MovieAttributes attribute)
    {
        double[] moviesAttributes = new double[movies.Count];
        for (int i = 0; i < movies.Count; i++)
        {
            switch (attribute)
            {
                case Movie.MovieAttributes.Earnings:
                    moviesAttributes[i] = movies[i].Earnings;
                    break;
                case Movie.MovieAttributes.ActorsPercent:
                    moviesAttributes[i] = movies[i].ActorsPercent;
                    break;
                case Movie.MovieAttributes.ReleaseYear:
                    moviesAttributes[i] = movies[i].ReleaseYear;
                    break;
                case Movie.MovieAttributes.Rating:
                    moviesAttributes[i] = movies[i].Rating;
                    break;
            }
        }

        return moviesAttributes;
    }

    /// <summary>
    /// Sorts list of Movie objects.
    /// </summary>
    /// <param name="moviesToSort">List of Movie objects to sort.</param>
    /// <param name="sortParameter">Value of MovieAttribute. Sorting will be by this parameter.</param>
    /// <param name="orderType">Shows order of sorting.</param>
    /// <returns>Sorted list of Movie objects.</returns>
    public static List<Movie> Sort(List<Movie> moviesToSort, Movie.MovieAttributes sortParameter, int orderType)
    {
        List<Movie> sortedMovies = new List<Movie>(moviesToSort);
        switch (sortParameter)
        {
            case Movie.MovieAttributes.Title:
            case Movie.MovieAttributes.Genre:
                string[] textAttributesToSort = GetStringMoviesAttributes(moviesToSort, sortParameter);
                for (int i = 0; i < sortedMovies.Count; i++)
                {
                    for (int j = 0; j < sortedMovies.Count - 1 - i; j++)
                    {
                        if (string.CompareOrdinal(textAttributesToSort[j], textAttributesToSort[j + 1]) > 0)
                        {
                            (textAttributesToSort[j], textAttributesToSort[j + 1]) =
                                (textAttributesToSort[j + 1], textAttributesToSort[j]);
                            (sortedMovies[j], sortedMovies[j + 1]) = (sortedMovies[j + 1], sortedMovies[j]);
                        }
                    }
                }

                break;
            default:
                double[] digitalAttributesToSort = GetDigitalMoviesAttributes(moviesToSort, sortParameter);
                for (int i = 0; i < sortedMovies.Count; i++)
                {
                    for (int j = 0; j < sortedMovies.Count - 1 - i; j++)
                    {
                        if (digitalAttributesToSort[j] > digitalAttributesToSort[j + 1])
                        {
                            (digitalAttributesToSort[j], digitalAttributesToSort[j + 1]) = (
                                digitalAttributesToSort[j + 1], digitalAttributesToSort[j]);
                            (sortedMovies[j], sortedMovies[j + 1]) = (sortedMovies[j + 1], sortedMovies[j]);
                        }
                    }
                }

                break;
        }

        if (orderType == 2)
        {
            sortedMovies.Reverse();
        }

        return sortedMovies;
    }
}