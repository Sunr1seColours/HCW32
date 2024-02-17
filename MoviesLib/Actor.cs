using System.Text;
using System.Text.Json.Serialization;

namespace MoviesLib;

/// <summary>
/// Class that represents information about one actor.
/// </summary>
public class Actor
{
    /// <summary>
    /// Property to get and set an ID of actor.
    /// </summary>
    [JsonPropertyName("actorId")]
    public string Id { get; set; }

    /// <summary>
    /// Property to get and set a name of actor.
    /// </summary>
    [JsonPropertyName("actorName")]
    public string Name { get; set; }

    /// <summary>
    /// Property to get and set a nationality of actor.
    /// </summary>
    [JsonPropertyName("nationality")]
    public string Nationality { get; set; }

    /// <summary>
    /// Property to get and set an earnings of actor.
    /// </summary>
    [JsonPropertyName("earnings")]
    public double Earnings { get; set; }

    /// <summary>
    /// Class constructor for Actor object without any parameters.
    /// </summary>
    public Actor()
    {
    }

    /// <summary>
    /// Event which fires when earnings of movie changes.
    /// </summary>
    public static event EventHandler<EarningChangedEventArgs> Updated;

    /// <summary>
    /// Creates string in JSON format of Actor object.
    /// </summary>
    /// <returns>String to be written in JSON file.</returns>
    public string ToJSON()
    {
        StringBuilder inJsonFormat = new StringBuilder();
        inJsonFormat.Append("      {\n");
        inJsonFormat.Append($"        \"actorId\": \"{Id}\",\n");
        inJsonFormat.Append($"        \"actorName\": \"{Name}\",\n");
        inJsonFormat.Append($"        \"nationality\": \"{Nationality}\",\n");
        inJsonFormat.Append($"        \"earnings\": {Earnings.ToString().Replace(',', '.')}\n");
        inJsonFormat.Append("      }");

        return inJsonFormat.ToString();
    }
}