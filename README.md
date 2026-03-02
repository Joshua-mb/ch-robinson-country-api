# CH Robinson Country Route API

## Project Overview

A C# .NET 10 Web API that returns the shortest overland route of countries a truck driver must travel through from the USA to a given destination country. The shortest path is found using Breadth-First Search (BFS) on a hardcoded adjacency graph of North and Central American countries.

**Live URL:** https://ch-robinson-country-api-bafyhye3fcgdffcd.eastus2-01.azurewebsites.net

---

## How to Use

Hit the endpoint with a 3-letter ISO country code (case-insensitive):

```
GET /{countryCode}
```

**Example request:**
```
GET /PAN
```

**Example response:**
```json
{
  "destination": "PAN",
  "path": ["USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN"]
}
```

---

## Success and Failure Responses

| Scenario | Status | Response |
|---|---|---|
| Valid country code | `200 OK` | JSON object with `destination` and `path` |
| USA entered | `200 OK` | `{ "destination": "USA", "path": ["USA"] }` |
| Invalid country code | `400 Bad Request` | `"The destination {code} is not a valid North American country code"` |

---

## How to Run Locally

**Prerequisites:** .NET 10 SDK

```bash
# Clone the repo
git clone <repo-url>
cd ch-robinson-country-api

# Run the API
dotnet run
```

The API will be available at `http://localhost:5000` and `https://localhost:5001`.

**Test it:**
```bash
curl http://localhost:5000/PAN
curl http://localhost:5000/usa
curl http://localhost:5000/ZZZ
```

---

## Design Decisions

- **C# .NET** — Chosen to align with C.H. Robinson's backend tech stack.
- **BFS for shortest path** — Guarantees the fewest border crossings between USA and the destination. Each country is a node; each shared border is an edge.
- **Separated into Models, Services, and Controllers** — Follows standard MVC layering for maintainability and testability.
- **Deployed on Azure App Service** — Matches C.H. Robinson's cloud infrastructure. CI/CD is handled via GitHub Actions on push to `main`.

---

## Assumptions

- The same assumptions as the React frontend app apply here.
- The graph only includes countries reachable overland from the USA through North and Central America (USA → Panama corridor).
- Country borders are treated as undirected edges (travel works both ways).
- Only one valid path corridor exists (no branching routes that reconnect), so BFS always returns the unique shortest path.

---

## File Structure

```
ch-robinson-country-api/
├── Controllers/
│   └── CountryController.cs   # GET /{countryCode} endpoint
├── Models/
│   └── PathResponse.cs        # Response model { destination, path }
├── Services/
│   ├── CountryGraph.cs        # Hardcoded border adjacency graph
│   └── BfsService.cs          # BFS shortest-path algorithm
├── Program.cs                 # App entry point, DI, CORS config
└── ch-robinson-country-api.csproj
```

---

## Tech Stack

| Layer | Technology |
|---|---|
| Language / Framework | C# .NET 10 Web API |
| Hosting | Azure App Service |
| CI/CD | GitHub Actions |
