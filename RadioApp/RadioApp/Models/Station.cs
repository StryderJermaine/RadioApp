namespace RadioApp.Models;

/// <summary>
/// Model class for a radio station
/// </summary>
public class Station
{
    /// <summary>
    /// Frequency of the station
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// Flag to indicate if a station is a favourite or not
    /// </summary>
    public bool IsFavourite { get; set; }
}