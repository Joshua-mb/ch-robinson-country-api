namespace ChRobinson.CountryApi.Models;

/// <summary>
/// Response model returned by the route-finding endpoint.
/// </summary>
public class PathResponse
{
    /// <summary>The destination country code (upper-cased).</summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// Ordered list of country codes from USA to the destination,
    /// inclusive of both endpoints.
    /// </summary>
    public List<string> Path { get; set; } = [];
}
