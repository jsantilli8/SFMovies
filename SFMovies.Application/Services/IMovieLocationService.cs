using SFMovies.Domain.Entities;

namespace SFMovies.Application.Services;

public interface IMovieLocationService
{
    Task<IEnumerable<MovieLocation>> GetAllLocationsAsync();
    Task<IEnumerable<MovieLocation>> SearchLocationsAsync(string searchTerm);
    Task<MovieLocation?> GetLocationByIdAsync(int id);
    Task<IEnumerable<string>> GetAutocompleteSuggestionsAsync(string searchTerm);
    Task<IEnumerable<MovieLocation>> GetLocationsInViewportAsync(double minLat, double maxLat, double minLng, double maxLng);
}
