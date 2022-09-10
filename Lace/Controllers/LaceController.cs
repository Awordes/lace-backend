using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lace.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LaceController: ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}