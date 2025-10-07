using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMovies.Application.Queries;
using SFMovies.Application.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace SFMovies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "LocationAccess")]
public class MovieLocationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MovieLocationsController> _logger;

    public MovieLocationsController(
        IMediator mediator,
        ILogger<MovieLocationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all movie locations",
        Description = "Returns all movie locations in San Francisco. Data is cached for performance."
    )]
    [ProducesResponseType(typeof(IEnumerable<MovieLocationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieLocationDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new GetAllMovieLocationsQuery(), 
                cancellationToken
            );
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving movie locations");
            return StatusCode(500, "An error occurred while retrieving movie locations");
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get movie location by ID",
        Description = "Returns a single movie location by its ID."
    )]
    [ProducesResponseType(typeof(MovieLocationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovieLocationDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetMovieLocationByIdQuery(id),
            cancellationToken
        );
        
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("search")]
    [SwaggerOperation(
        Summary = "Search movie locations by title",
        Description = "Returns movie locations that match the given title. Results are cached for performance."
    )]
    [ProducesResponseType(typeof(IEnumerable<MovieLocationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MovieLocationDto>>> Search(
        [FromQuery] string title,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new SearchMovieLocationsByTitleQuery(title),
            cancellationToken
        );
        return Ok(result);
    }
}
