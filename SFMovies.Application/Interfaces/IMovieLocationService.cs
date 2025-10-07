using SFMovies.Application.DTOs;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Interfaces;

public interface IMovieLocationService
{
    Task<IEnumerable<MovieLocationDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<MovieLocationDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<MovieLocationDto>> SearchByTitleAsync(string title, CancellationToken cancellationToken);
}