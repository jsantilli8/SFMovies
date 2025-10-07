using MediatR;
using SFMovies.Application.Queries;
using SFMovies.Application.Interfaces;
using SFMovies.Application.DTOs;

namespace SFMovies.Application.Handlers;

public class GetMovieLocationByIdHandler : IRequestHandler<GetMovieLocationByIdQuery, MovieLocationDto?>
{
    private readonly IMovieLocationService _service;
    public GetMovieLocationByIdHandler(IMovieLocationService service)
    {
        _service = service;
    }
    public async Task<MovieLocationDto?> Handle(GetMovieLocationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id, cancellationToken);
    }
}
