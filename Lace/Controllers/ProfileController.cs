using Lace.Application.CQRS.Profile.Queries;
using Lace.Application.CQRS.Profile.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lace.Controllers;

public class ProfileController: LaceController
{
    [HttpGet("/[controller]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProfileViewModel>> Get(Guid Id)
    {
        try
        {
            return Ok(await Mediator.Send(new GetProfileQuery { Id = Id }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProfileListViewModel>> GetList([FromQuery] GetProfilesQuery query)
    {
        try
        {
            return Ok(await Mediator.Send(query));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}