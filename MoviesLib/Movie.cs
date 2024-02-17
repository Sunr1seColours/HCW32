using System.Text;
using System.Text.Json.Serialization;

namespace MoviesLib;

/// <summary>
/// Class which contains information about movie.
/// </summary>
public class Movie
{
    /// <summary>
    /// Reacts on changing in actors' earnings from film.
    /// </summary>
    /// <param name="sender">Object where event happened.</param>
    /// <param name="newEarning">Information about new earnings.</param>
    private void OnNewActorsSalary(object? sender, EarningChangedEventArgs newEarning)
    {
        double newSalary = Earnings / Actors.Length;

        foreach (Actor actor in Actors)
        {
            actor.Earnings = newSalary;
        }
    }

    /// <summary>
    /// Enumeration which contains movie attributes.
    /// </summary>
    public enum MovieAttributes
    {
        Title,
        Earnings,
        ActorsPercent,
        ReleaseYear,
        Genre,
        Rating
    }

    /// <summary>
    /// Property to get ID of movie.
    /// </summary>
    [JsonPropertyName("movieId")]
    public string Id { get; init; }

    /// <summary>
    /// Property to get and set title of movie.
    /// </summary>
    [JsonPropertyName("movieTitle")]
    public string Title { get; set; }

    /// <summary>
    /// Property to get and set information about movie's earnings.
    /// </summary>
    [JsonPropertyName("earnings")]
    public double Earnings { get; set; }

    /// <summary>
    /// Property to get and set actors percent of movie.
    /// </summary>
    [JsonPropertyName("actorsPercent")]
    public double ActorsPercent { get; set; }

    /// <summary>
    /// Property to get and set release year of movie.
    /// </summary>
    [JsonPropertyName("releaseYear")]
    public int ReleaseYear { get; set; }

    /// <summary>
    /// Property to get and set genre of movie.
    /// </summary>
    [JsonPropertyName("genre")]
    public string Genre { get; set; }

    /// <summary>
    /// Property to get and set rating of movie.
    /// </summary>
    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    /// <summary>
    /// Property to get and set array of actors which are in the movie.
    /// </summary>
    [JsonPropertyName("actors")]
    public Actor[] Actors { get; set; }

    /// <summary>
    /// Constructor of Movie object. Creates a subscription for changing earnings of film instantly.
    /// </summary>
    public Movie()
    {
        Updated += OnNewActorsSalary;
    }

    /// <summary>
    /// Creates string in JSON format of Movie object.
    /// </summary>
    /// <returns>String which is good to write in JSON file.</returns>
    public string ToJSON()
    {
        StringBuilder inJsonFormat = new StringBuilder();
        inJsonFormat.Append("  {\n");
        inJsonFormat.Append($"    \"movieId\": \"{Id}\",\n");
        inJsonFormat.Append($"    \"movieTitle\": \"{Title}\",\n");
        inJsonFormat.Append($"    \"earnings\": {Earnings.ToString().Replace(',', '.')},\n");
        inJsonFormat.Append($"    \"actorsPercent\": {ActorsPercent.ToString().Replace(',', '.')},\n");
        inJsonFormat.Append($"    \"releaseYear\": {ReleaseYear.ToString().Replace(',', '.')},\n");
        inJsonFormat.Append($"    \"genre\": \"{Genre}\",\n");
        inJsonFormat.Append($"    \"rating\": {Rating.ToString().Replace(',', '.')},\n");
        inJsonFormat.Append("    \"actors\": [\n");
        foreach (Actor actor in Actors)
        {
            inJsonFormat.Append(actor.ToJSON());
            inJsonFormat.Append(",\n");
        }

        inJsonFormat.Remove(inJsonFormat.Length - 2, 2);
        inJsonFormat.Append("\n    ]\n");
        inJsonFormat.Append("  }");

        return inJsonFormat.ToString();
    }

    /// <summary>
    /// Event which fires when update in Movie information happens.
    /// </summary>
    public static event EventHandler<DataUpdateEventArgs> MovieInfoUpdated;

    /// <summary>
    /// Event which fires when earnings of movie changes.
    /// </summary>
    public static event EventHandler<EarningChangedEventArgs> Updated;

    /// <summary>
    /// Fires 'MovieInfoUpdated' event.
    /// </summary>
    /// <param name="updateInfo">Information about update.</param>
    public static void MovieInfoUpdateFire(DataUpdateEventArgs updateInfo)
    {
        MovieInfoUpdated?.Invoke(null, updateInfo);
    }

    /// <summary>
    /// Changes one attribute of movie. ID and Actors can't be changed.
    /// </summary>
    /// <param name="attribute">Type of movie's attribute.</param>
    /// <param name="newValue">New value of movie's attribute.</param>
    /// <exception cref="ArgumentNullException">New value is null or empty.</exception>
    /// <exception cref="ArgumentException">Wrong new value of actors percent.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Wrong value of year when film was released.</exception>
    public void ChangeOneAttribute(MovieAttributes attribute, string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
        {
            throw new ArgumentNullException(nameof(newValue), "cant' be null.");
        }

        switch (attribute)
        {
            case MovieAttributes.Title:
                Title = newValue;
                break;
            case MovieAttributes.Earnings:
                Earnings = Convert.ToDouble(newValue);
                Updated?.Invoke(this, new EarningChangedEventArgs());
                break;
            case MovieAttributes.ActorsPercent:
                double actorsPercent = Convert.ToDouble(newValue);
                if (actorsPercent < 0)
                {
                    throw new ArgumentException("Actors percent must be positive.");
                }

                ActorsPercent = actorsPercent;
                break;
            case MovieAttributes.ReleaseYear:
                int year = Convert.ToInt32(newValue);
                if (year < 1895 && year > 2024)
                {
                    throw new ArgumentOutOfRangeException(nameof(newValue),
                        "Film's release year must be in [1895; 2024].");
                }

                ReleaseYear = year;
                break;
            case MovieAttributes.Genre:
                Title = newValue;
                break;
            case MovieAttributes.Rating:
                Rating = Convert.ToDouble(newValue);
                break;
        }
    }

    /// <summary>
    /// Get name of movie's attribute.
    /// </summary>
    /// <param name="attribute">Value of MovieAttributes enumeration.</param>
    /// <returns>Name of movie's attribute.</returns>
    public static string GetNameOfAttribute(MovieAttributes attribute)
    {
        switch (attribute)
        {
            case MovieAttributes.Title:
                return "title";
            case MovieAttributes.Earnings:
                return "earnings";
            case MovieAttributes.ActorsPercent:
                return "actors percent";
            case MovieAttributes.ReleaseYear:
                return "release year";
            case MovieAttributes.Genre:
                return "genre";
            case MovieAttributes.Rating:
                return "rating";
            default:
                return "";
        }
    }
}