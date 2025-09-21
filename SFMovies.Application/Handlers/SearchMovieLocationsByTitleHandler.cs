using MediatR;
using SFMovies.Application.Queries;
using SFMovies.Application.Services;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Handlers;

public class SearchMovieLocationsByTitleHandler : IRequestHandler<SearchMovieLocationsByTitleQuery, IEnumerable<MovieLocation>>
{
    private readonly IMovieLocationService _service;
    public SearchMovieLocationsByTitleHandler(IMovieLocationService service)
    {
        _service = service;
    }
    public async Task<IEnumerable<MovieLocation>> Handle(SearchMovieLocationsByTitleQuery request, CancellationToken cancellationToken)
    {
        return await _service.SearchLocationsAsync(request.Title);
    }
}
