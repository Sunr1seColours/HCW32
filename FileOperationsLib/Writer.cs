using System.Text;
using System.Text.Json;
using MoviesLib;

namespace FileOperationsLib;

/// <summary>
/// Class with operations for writing JSON file.
/// </summary>
public class Writer
{
    /// <summary>
    /// Amount of updates in collection about Movie objects.
    /// </summary>
    private int UpdatesCount { get; set; }

    /// <summary>
    /// Base constructor with no parameters.
    /// </summary>
    public Writer()
    {
        UpdatesCount = 0;
    }

    /// <summary>
    /// Reacts on update in collection of Movie objects.
    /// </summary>
    /// <param name="movies">Collection of Movie objects to write into file.</param>
    public void OnGetNewUpdate(List<Movie> movies)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        StringBuilder moviesReadyToWrite = new StringBuilder();
        moviesReadyToWrite.Append("[\n");
        foreach (Movie movie in movies)
        {
            moviesReadyToWrite.Append(movie.ToJSON());
            moviesReadyToWrite.Append(",\n");
        }

        moviesReadyToWrite.Append(']');

        string path = $"9V_tmp{UpdatesCount}.json";
        using (StreamWriter saver = new StreamWriter(path))
        {
            saver.Write(moviesReadyToWrite.ToString());
        }

        UpdatesCount++;
    }
}