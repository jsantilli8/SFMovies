using MediatR;
using SFMovies.Application.DTOs;

namespace SFMovies.Application.Queries;

public record GetMovieLocationByIdQuery(int Id) : IRequest<MovieLocationDto?>;
