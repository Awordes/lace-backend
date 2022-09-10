using Lace.Application.CQRS.Category.Commands;
using Lace.Application.CQRS.Category.Queries;
using Lace.Application.CQRS.Category.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lace.Controllers;

public class CategoryController: LaceController
{
    [HttpPost("/[controller]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
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
    public async Task<ActionResult<CategoryViewModel>> Get(Guid id)
    {
        try
        {
            return Ok(await Mediator.Send(new GetCategoryQuery { Id = id }));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryListViewModel>> GetList([FromQuery] GetCategoriesQuery query)
    {
        try
        {
            return Ok(await Mediator.Send(query));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}