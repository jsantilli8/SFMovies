using MediatR;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Queries;

public record GetAllMovieLocationsQuery() : IRequest<IEnumerable<MovieLocation>>;
