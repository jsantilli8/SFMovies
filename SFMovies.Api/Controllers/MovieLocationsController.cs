using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMovies.Application.Queries;
using SFMovies.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace SFMovies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "CanViewLocations")]
public class MovieLocationsController : ControllerBase
{
    private readonly IMediator _mediator;
    public MovieLocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all movie locations", Description = "Returns all movie locations in San Francisco.")]
    public async Task<IEnumerable<MovieLocation>> GetAll()
        => await _mediator.Send(new GetAllMovieLocationsQuery());

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get movie location by ID", Description = "Returns a single movie location by its ID.")]
    public async Task<ActionResult<MovieLocation?>> GetById(int id)
    {
        var result = await _mediator.Send(new GetMovieLocationByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search movie locations by title", Description = "Returns movie locations that match the given title.")]
    public async Task<IEnumerable<MovieLocation>> Search([FromQuery] string title)
        => await _mediator.Send(new SearchMovieLocationsByTitleQuery(title));
}
