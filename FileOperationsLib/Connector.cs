using System.Text.RegularExpressions;

namespace FileOperationsLib;

/// <summary>
/// Makes connection to file.
/// </summary>
public class Connector
{
    /// <summary>
    /// Private pole which contains path to file which will be read.
    /// </summary>
    private static string? _pathToFileToRead;

    /// <summary>
    /// Property to get and set path to file for reading.
    /// </summary>
    /// <exception cref="FileNotFoundException">File doesn't exist.</exception>
    /// <exception cref="ArgumentException">File doesn't have a .json extension or user entered nothing.</exception>
    public static string? PathToFileToRead
    {
        get => _pathToFileToRead;

        set
        {
            Regex extensionChecker = new Regex(@"(\.json)$");
            if (extensionChecker.IsMatch(value ?? ""))
            {
                if (!File.Exists(value))
                {
                    throw new FileNotFoundException("There is no file you're looking for.");
                }

                _pathToFileToRead = value;
            }
            else
            {
                if (!string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("File doesn't have a .json extension.");
                }
                else
                {
                    throw new ArgumentException("You must enter something.");
                }
            }
        }
    }
}