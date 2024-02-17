namespace MoviesLib;

/// <summary>
/// Inheritor of EventArgs class. Contains information about time when object was created or
/// time which was given in a constructor and the collection of Movie where change was made.
/// </summary>
public class DataUpdateEventArgs : EventArgs
{
    /// <summary>
    /// Property to get time that object contains. 
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Property to get collection of Movie objects.
    /// </summary>
    public List<Movie> Contents { get; }

    /// <summary>
    /// Constructor which creates object with current time.
    /// </summary>
    /// <param name="contents">Collection of Movie objects.</param>
    public DataUpdateEventArgs(List<Movie> contents) : this(DateTime.Now, contents)
    {
    }

    /// <summary>
    /// Constructor with given time.
    /// </summary>
    /// <param name="time">Time which is given.</param>
    /// <param name="contents">Collection of Movie objects.</param>
    public DataUpdateEventArgs(DateTime time, List<Movie> contents)
    {
        Time = time;
        Contents = contents;
    }
}