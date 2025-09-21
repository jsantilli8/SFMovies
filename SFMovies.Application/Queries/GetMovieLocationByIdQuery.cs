using MediatR;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Queries;

public record GetMovieLocationByIdQuery(int Id) : IRequest<MovieLocation?>;
