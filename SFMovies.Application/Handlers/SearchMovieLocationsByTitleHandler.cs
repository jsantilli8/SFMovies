using MediatR;
using SFMovies.Application.Queries;
using SFMovies.Application.Interfaces;
using SFMovies.Application.DTOs;

namespace SFMovies.Application.Handlers;

public class SearchMovieLocationsByTitleHandler : IRequestHandler<SearchMovieLocationsByTitleQuery, IEnumerable<MovieLocationDto>>
{
    private readonly IMovieLocationService _service;
    public SearchMovieLocationsByTitleHandler(IMovieLocationService service)
    {
        _service = service;
    }
    public async Task<IEnumerable<MovieLocationDto>> Handle(SearchMovieLocationsByTitleQuery request, CancellationToken cancellationToken)
    {
        return await _service.SearchByTitleAsync(request.Title, cancellationToken);
    }
}
