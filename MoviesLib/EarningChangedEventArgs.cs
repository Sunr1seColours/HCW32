namespace MoviesLib;

/// <summary>
/// Inheritor of EventArgs class.
/// </summary>
public class EarningChangedEventArgs : EventArgs
{
    /// <summary>
    /// Property to get time which object contains.
    /// </summary>
    public DateTime Time { get; }
    
    /// <summary>
    /// Constructor which creates object with current time.
    /// </summary>
    public EarningChangedEventArgs() : this (DateTime.Now) { }

    /// <summary>
    /// Constructor which creates object with given time.
    /// </summary>
    /// <param name="time"></param>
    public EarningChangedEventArgs(DateTime time)
    {
        Time = time;
    }
}