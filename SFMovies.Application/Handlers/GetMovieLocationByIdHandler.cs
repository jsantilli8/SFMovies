using MediatR;
using SFMovies.Application.Queries;
using SFMovies.Application.Services;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Handlers;

public class GetMovieLocationByIdHandler : IRequestHandler<GetMovieLocationByIdQuery, MovieLocation?>
{
    private readonly IMovieLocationService _service;
    public GetMovieLocationByIdHandler(IMovieLocationService service)
    {
        _service = service;
    }
    public async Task<MovieLocation?> Handle(GetMovieLocationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetLocationByIdAsync(request.Id);
    }
}
