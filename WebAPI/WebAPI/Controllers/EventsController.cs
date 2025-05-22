using MediatR;
using Microsoft.AspNetCore.Mvc;
using Events_Web_Application.src.Application.Events.Commands;
using Events_Web_Application.src.Application.Events.Queries;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Enums;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.WebAPI.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Events_Web_Application.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<ActionResult<ICollection<EventDTO>>> GetAll(CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var events = await _mediator.Send(new GetAllEventQuery(page, pageSize), ct);

        return Ok(events);
    }

    [HttpGet("name")]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<ActionResult<ICollection<EventDTO>>> GetByName(CancellationToken ct, [FromQuery] string name)
    {
        var events = await _mediator.Send(new GetByNameEventQuery(name), ct);
        return Ok(events);
    }


    [HttpGet("date")]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<ActionResult<ICollection<EventDTO>>> GetByDate(CancellationToken ct, [FromQuery] DateTime date, [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var events = await _mediator.Send(new GetByDateEventQuery(date, page, pageSize), ct);
        return Ok(events);
    }

    [HttpGet("place")]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<ActionResult<ICollection<EventDTO>>> GetByPlace(CancellationToken ct, [FromQuery] string place, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var events = await _mediator.Send(new GetByPlaceEventQuery(place, page, pageSize), ct);
        return Ok(events);
    }

    [HttpGet("category")]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<ActionResult<ICollection<EventDTO>>> GetByCategory(CancellationToken ct, [FromQuery] Category category, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var events = await _mediator.Send(new GetByCategoryEventQuery(category, page, pageSize), ct);
        return Ok(events);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<ActionResult<EventDTO>> GetById(int id, CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetByIdEventQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<int>> Create(CancellationToken ct, [FromBody] CreateEventCommand command)
    {
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, CancellationToken ct, [FromBody] UpdateEventCommand command)
    {
        command.Id = id;
        await _mediator.Send(command, ct);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(CancellationToken ct, int id)
    {
        await _mediator.Send(new DeleteEventCommand(id), ct);
        return NoContent();
    }

    [HttpPost("{id}/photo")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AddPhoto(int id, CancellationToken ct, [FromBody] AddPhotoEventCommand command)
    {
        await _mediator.Send(command, ct);
        return NoContent();
    }
}
