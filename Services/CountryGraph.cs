namespace ChRobinson.CountryApi.Services;

/// <summary>
/// Hardcoded adjacency graph of North/Central American countries reachable
/// overland from the USA.  Each key is a 3-letter ISO 3166-1 alpha-3 country
/// code; the value is the list of countries that share a land border with it.
///
/// Graph structure (undirected):
///
///   CAN ── USA ── MEX ── BLZ
///                  │       │
///                 GTM ─────┘
///                  │  \
///                 SLV  HND ── NIC ── CRI ── PAN
///                  │___/
///
/// BFS will traverse these edges to find the shortest hop-count path.
/// </summary>
public static class CountryGraph
{
    public static readonly Dictionary<string, List<string>> Adjacency =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["CAN"] = ["USA"],
            ["USA"] = ["CAN", "MEX"],
            ["MEX"] = ["USA", "GTM", "BLZ"],
            ["BLZ"] = ["MEX", "GTM"],
            ["GTM"] = ["MEX", "BLZ", "SLV", "HND"],
            ["SLV"] = ["GTM", "HND"],
            ["HND"] = ["GTM", "SLV", "NIC"],
            ["NIC"] = ["HND", "CRI"],
            ["CRI"] = ["NIC", "PAN"],
            ["PAN"] = ["CRI"],
        };
}
