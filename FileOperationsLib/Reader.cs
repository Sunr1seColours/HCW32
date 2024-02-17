using System.Text.Json;
using MoviesLib;

namespace FileOperationsLib;

/// <summary>
/// Represents operation over reading JSON file.
/// </summary>
public static class Reader
{
    /// <summary>
    /// Reads JSON file and deserialize it.
    /// </summary>
    /// <param name="path">Path to file.</param>
    /// <returns>Collection of Movie objects and information about it.</returns>
    public static (List<Movie>?, DataUpdateEventArgs firstUpdate) ReadFile(string path)
    {
        string content = File.ReadAllText(path);
        List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(content);
        DataUpdateEventArgs firstUpdate = new DataUpdateEventArgs(movies);

        return (movies, firstUpdate);
    }
}