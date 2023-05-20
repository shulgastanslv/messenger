using MediatR;

namespace Presentation.Controllers;

public abstract class ApiControllerBase
{
    private ISender? _mediator;
    protected ApiControllerBase(ISender mediator)
    {
        _mediator = mediator;
    }
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}