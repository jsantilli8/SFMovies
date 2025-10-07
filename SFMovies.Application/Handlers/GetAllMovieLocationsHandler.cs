using MediatR;
using SFMovies.Application.Queries;
using SFMovies.Application.Interfaces;
using SFMovies.Application.DTOs;

namespace SFMovies.Application.Handlers;

public class GetAllMovieLocationsHandler : IRequestHandler<GetAllMovieLocationsQuery, IEnumerable<MovieLocationDto>>
{
    private readonly IMovieLocationService _service;
    public GetAllMovieLocationsHandler(IMovieLocationService service)
    {
        _service = service;
    }
    public async Task<IEnumerable<MovieLocationDto>> Handle(GetAllMovieLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(cancellationToken);
    }
}
