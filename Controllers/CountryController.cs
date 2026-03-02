using ChRobinson.CountryApi.Models;
using ChRobinson.CountryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChRobinson.CountryApi.Controllers;

[ApiController]
[Route("/")]
public class CountryController(BfsService bfs) : ControllerBase
{
    /// <summary>
    /// GET /{countryCode}
    /// Returns the shortest overland route from USA to the requested country.
    /// </summary>
    /// <param name="countryCode">ISO 3166-1 alpha-3 code (case-insensitive).</param>
    [HttpGet("{countryCode}")]
    public IActionResult GetRoute(string countryCode)
    {
        var code = countryCode.ToUpper();

        // Validate that the code exists in our graph.
        if (!bfs.IsValidCountry(code))
            return BadRequest($"The destination {code} is not a valid North American country code");

        // Run BFS from USA to the requested destination.
        var path = bfs.FindShortestPath(code);

        // path will always be non-null here because every node in the graph
        // is reachable from USA, but we guard defensively anyway.
        if (path is null)
            return BadRequest($"The destination {code} is not a valid North American country code");

        return Ok(new PathResponse
        {
            Destination = code,
            Path = path
        });
    }
}
