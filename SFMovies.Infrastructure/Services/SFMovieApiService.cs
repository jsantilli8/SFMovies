using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SFMovies.Application.Interfaces;
using SFMovies.Application.DTOs;

namespace SFMovies.Infrastructure.Services;

public class SFMovieApiService : IMovieLocationService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<SFMovieApiService> _logger;
    private readonly string _apiUrl;
    private const string CACHE_KEY_ALL = "MovieLocations_All";
    private const string CACHE_KEY_SEARCH = "MovieLocations_Search_{0}";
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(6);

    public SFMovieApiService(
        HttpClient httpClient,
        IMemoryCache cache,
        IConfiguration configuration,
        ILogger<SFMovieApiService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
        _apiUrl = configuration["ExternalApis:SFMovies"] ?? 
                 "https://data.sfgov.org/resource/yitu-d5am.json";
    }

    public async Task<IEnumerable<MovieLocationDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(CACHE_KEY_ALL, out List<MovieLocationDto> cachedLocations))
        {
            _logger.LogInformation("Returning movie locations from cache");
            return cachedLocations;
        }

        try
        {
            _logger.LogInformation("Fetching movie locations from API");
            var response = await _httpClient.GetFromJsonAsync<List<SFMovieApiResponse>>(_apiUrl, cancellationToken);
            var locations = response?.Select(MapToDto).ToList() ?? new List<MovieLocationDto>();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(CACHE_DURATION)
                .RegisterPostEvictionCallback((key, value, reason, state) => 
                {
                    _logger.LogInformation($"Cache entry {key} was evicted due to {reason}");
                });

            _cache.Set(CACHE_KEY_ALL, locations, cacheOptions);
            return locations;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching data from SF Movies API");
            return _cache.Get<List<MovieLocationDto>>(CACHE_KEY_ALL) ?? new List<MovieLocationDto>();
        }
    }

    public async Task<MovieLocationDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var all = await GetAllAsync(cancellationToken);
        return all.FirstOrDefault(m => m.Id == id);
    }

    public async Task<IEnumerable<MovieLocationDto>> SearchByTitleAsync(
        string title, 
        CancellationToken cancellationToken)
    {
        var cacheKey = string.Format(CACHE_KEY_SEARCH, title.ToLower());

        if (_cache.TryGetValue(cacheKey, out List<MovieLocationDto> cachedResults))
        {
            _logger.LogInformation($"Returning search results from cache for title: {title}");
            return cachedResults;
        }

        try
        {
            var query = $"{_apiUrl}?$where=lower(title) like lower('%{title}%')";
            var response = await _httpClient.GetFromJsonAsync<List<SFMovieApiResponse>>(query, cancellationToken);
            var results = response?.Select(MapToDto).ToList() ?? new List<MovieLocationDto>();

            _cache.Set(cacheKey, results, TimeSpan.FromHours(1));
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error searching movies with title: {title}");
            return _cache.Get<List<MovieLocationDto>>(cacheKey) ?? new List<MovieLocationDto>();
        }
    }

    private static MovieLocationDto MapToDto(SFMovieApiResponse apiResponse)
    {
        return new MovieLocationDto
        {
            Id = Math.Abs(apiResponse.title.GetHashCode()),
            Title = apiResponse.title,
            ReleaseYear = int.TryParse(apiResponse.release_year, out var year) ? year : null,
            Locations = apiResponse.locations,
            FunFacts = apiResponse.fun_facts,
            ProductionCompany = apiResponse.production_company,
            Distributor = apiResponse.distributor,
            Director = apiResponse.director,
            Latitude = double.TryParse(apiResponse.latitude, out var lat) ? lat : null,
            Longitude = double.TryParse(apiResponse.longitude, out var lng) ? lng : null
        };
    }
}

public class SFMovieApiResponse
{
    public string title { get; set; } = string.Empty;
    public string release_year { get; set; } = string.Empty;
    public string locations { get; set; } = string.Empty;
    public string? fun_facts { get; set; }
    public string? production_company { get; set; }
    public string? distributor { get; set; }
    public string? director { get; set; }
    public string? writer { get; set; }
    public string? actor_1 { get; set; }
    public string? actor_2 { get; set; }
    public string? actor_3 { get; set; }
    public string? latitude { get; set; }
    public string? longitude { get; set; }
}