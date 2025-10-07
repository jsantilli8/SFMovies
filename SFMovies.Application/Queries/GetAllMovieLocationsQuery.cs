using MediatR;
using SFMovies.Application.DTOs;

namespace SFMovies.Application.Queries;

public record GetAllMovieLocationsQuery() : IRequest<IEnumerable<MovieLocationDto>>;
