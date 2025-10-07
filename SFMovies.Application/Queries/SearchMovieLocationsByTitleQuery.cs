using MediatR;
using SFMovies.Application.DTOs;

namespace SFMovies.Application.Queries;

public record SearchMovieLocationsByTitleQuery(string Title) : IRequest<IEnumerable<MovieLocationDto>>;
