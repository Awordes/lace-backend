using Lace.Application.CQRS.ProfileAttribute.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Lace.Controllers;

public class ProfileAttributeController: LaceController
{
    [HttpPost("/[controller]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Create(CreateProfileAttributeCommand command)
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
}