using Lace.Application.CQRS.Category.Queries;
using Lace.Application.CQRS.User.Commands;
using Lace.Application.CQRS.User.Queries;
using Lace.Application.CQRS.User.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lace.Controllers;

public class UserController: LaceController
{
    [HttpPost("/[controller]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        try
        {
            await Mediator.Send(command);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/[controller]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserViewModel>> Get(Guid id)
    {
        try
        {
            return Ok(await Mediator.Send(new GetUserQuery { Id = id }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserViewModel>> Authorize(AuthorizeUserCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            
            if (result is not null)
                return Ok(result);
            
            return Unauthorized();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}