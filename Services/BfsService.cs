namespace ChRobinson.CountryApi.Services;

/// <summary>
/// Implements Breadth-First Search (BFS) to find the shortest path between
/// two countries in the adjacency graph.
///
/// How BFS works here:
///   1. Start by enqueuing the source node ("USA") along with its path so far.
///   2. Dequeue the front node.  If it is the destination, we're done — BFS
///      guarantees this is the shortest path (fewest border crossings).
///   3. Otherwise, add every unvisited neighbour to the queue, extending the
///      recorded path by one hop.
///   4. Mark nodes as visited immediately when they are enqueued to prevent
///      processing the same node via a longer route.
///   5. If the queue empties without finding the destination, no path exists.
/// </summary>
public class BfsService
{
    /// <summary>
    /// Returns the shortest list of country codes from <c>USA</c> to
    /// <paramref name="destination"/>, or <c>null</c> if unreachable.
    /// </summary>
    public List<string>? FindShortestPath(string destination)
    {
        const string start = "USA";

        // Normalise to upper-case so lookups are consistent.
        destination = destination.ToUpper();

        // Edge case: starting country is the destination.
        if (destination == start)
            return [start];

        // visited tracks which nodes have already been enqueued, preventing
        // cycles and redundant processing.
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { start };

        // Each queue item is the full path taken to reach the current node.
        // Storing the path (not just the node) lets us reconstruct the route
        // without a separate "parent" map.
        var queue = new Queue<List<string>>();
        queue.Enqueue([start]);

        while (queue.Count > 0)
        {
            var currentPath = queue.Dequeue();
            var currentNode = currentPath[^1]; // last element in path

            // Expand each neighbour of the current node.
            foreach (var neighbour in CountryGraph.Adjacency[currentNode])
            {
                if (visited.Contains(neighbour))
                    continue; // already found a shorter-or-equal path here

                var newPath = new List<string>(currentPath) { neighbour };

                // BFS level-by-level guarantees the first time we reach the
                // destination it is via the shortest route.
                if (string.Equals(neighbour, destination, StringComparison.OrdinalIgnoreCase))
                    return newPath;

                visited.Add(neighbour);
                queue.Enqueue(newPath);
            }
        }

        // No path found (destination is disconnected from USA).
        return null;
    }

    /// <summary>Returns true if <paramref name="code"/> exists in the graph.</summary>
    public bool IsValidCountry(string code) =>
        CountryGraph.Adjacency.ContainsKey(code);
}
