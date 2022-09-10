using Lace.Application.CQRS.DictionaryElement.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Lace.Controllers;

public class DictionaryElementController: LaceController
{
    [HttpPost("/[controller]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Create(CreateDictionaryElementCommand command)
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