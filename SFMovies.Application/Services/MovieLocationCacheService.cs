using Microsoft.Extensions.Caching.Memory;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Services;

public class MovieLocationCacheService : IMovieLocationService
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "MovieLocations";
    private List<MovieLocation> _locations;

    public MovieLocationCacheService(IMemoryCache cache)
    {
        _cache = cache;
        _locations = LoadFromDataset(); // Sincrónico, en memoria
    }

    // Solo para test
    public void SetLocationsForTest(List<MovieLocation> locations)
    {
        _locations = locations;
    }

    public Task<IEnumerable<MovieLocation>> GetAllLocationsAsync()
    {
        return Task.FromResult(_locations.AsEnumerable());
    }

    public Task<IEnumerable<MovieLocation>> SearchLocationsAsync(string searchTerm)
    {
        var result = _locations.Where(x => x.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(result);
    }

    public Task<MovieLocation?> GetLocationByIdAsync(int id)
    {
        var result = _locations.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<string>> GetAutocompleteSuggestionsAsync(string searchTerm)
    {
        var result = _locations.Select(x => x.Title)
            .Where(t => t.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .Distinct()
            .Take(10);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<MovieLocation>> GetLocationsInViewportAsync(double minLat, double maxLat, double minLng, double maxLng)
    {
        var result = _locations.Where(x => x.Latitude >= minLat && x.Latitude <= maxLat && x.Longitude >= minLng && x.Longitude <= maxLng);
        return Task.FromResult(result);
    }

    private List<MovieLocation> LoadFromDataset()
    {
        // TODO: Implementar la carga real desde DataSF (API, CSV, JSON, etc.)
        return new List<MovieLocation>();
    }
}
