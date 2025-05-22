using MediatR;
using Microsoft.AspNetCore.Mvc;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.WebAPI.Pagination;
using Microsoft.AspNetCore.Authorization;

namespace Events_Web_Application.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("event/{eventId}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<ICollection<UserDTO>>> GetUsersByEvent(int eventId, CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetByEventUserQuery(eventId, page, pageSize);
        var users = await _mediator.Send(query, ct);
        return Ok(users);
    }


    [HttpGet("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<UserDTO>> GetById(int id, CancellationToken ct)
    {
        var user = await _mediator.Send(new GetByIdUserQuery(id), ct);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpGet("Changes")]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<ActionResult<ICollection<ChangesDTO>>> GetChanges(CancellationToken ct)
    {
        var changes = await _mediator.Send(new GetChangesQuery(), ct);
        return Ok(changes);
    }


    [HttpPost("cancel")]
    [Authorize(Policy = "AtLeastUser")]
    public async Task<IActionResult> Cancel(CancellationToken ct, [FromBody] CancelParticipationUserCommand command)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

}
