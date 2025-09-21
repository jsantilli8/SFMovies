using MediatR;
using SFMovies.Application.Queries;
using SFMovies.Application.Services;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Handlers;

public class GetAllMovieLocationsHandler : IRequestHandler<GetAllMovieLocationsQuery, IEnumerable<MovieLocation>>
{
    private readonly IMovieLocationService _service;
    public GetAllMovieLocationsHandler(IMovieLocationService service)
    {
        _service = service;
    }
    public async Task<IEnumerable<MovieLocation>> Handle(GetAllMovieLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllLocationsAsync();
    }
}
