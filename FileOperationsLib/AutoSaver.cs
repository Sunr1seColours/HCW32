using MoviesLib;

namespace FileOperationsLib;

/// <summary>
/// Class which shows when information of Movies collection updates.
/// </summary>
public class AutoSaver
{
    /// <summary>
    /// Collection of Movies updates.
    /// </summary>
    public List<DataUpdateEventArgs> Updates { get; set; }
    
    /// <summary>
    /// Base constructor. Creates new collection of Movies updates.
    /// </summary>
    public AutoSaver()
    {
        Updates = new List<DataUpdateEventArgs>();
    }

    /// <summary>
    /// Delegate, which argument is a collection of Movie objects.
    /// </summary>
    public delegate void UpdateHappened(List<Movie> updatedData);

    /// <summary>
    /// Event with 'UpdateHappened' delegate at the base
    /// </summary>
    public event UpdateHappened GetNewUpdate;
    
    /// <summary>
    /// Reacts on updates in collection of Movie objects.
    /// </summary>
    /// <param name="source">Where event has happened.</param>
    /// <param name="dataUpdate">Information about update of collection of Movie object.</param>
    public void OnDataUpdate(object? source, DataUpdateEventArgs dataUpdate)
    {
        Updates.Add(dataUpdate);
        if (Updates.Count == 1)
        {
            GetNewUpdate?.Invoke(dataUpdate.Contents);
        }
        else
        {
            if (Updates[^2].Time.Minute == Updates[^1].Time.Minute)
            {
                if (Updates[^1].Time.Second - Updates[^2].Time.Second <= 15)
                {
                    GetNewUpdate?.Invoke(dataUpdate.Contents);
                }
            }
            else if (Updates[^1].Time.Minute - Updates[^2].Time.Minute == 1)
            {
                if (Updates[^1].Time.Second + 60 - Updates[^2].Time.Second <= 15)
                {
                    GetNewUpdate?.Invoke(dataUpdate.Contents);
                }
            }
        }
    }
}