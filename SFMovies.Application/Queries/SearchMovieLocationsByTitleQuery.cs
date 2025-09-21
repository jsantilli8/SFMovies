using MediatR;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Queries;

public record SearchMovieLocationsByTitleQuery(string Title) : IRequest<IEnumerable<MovieLocation>>;
