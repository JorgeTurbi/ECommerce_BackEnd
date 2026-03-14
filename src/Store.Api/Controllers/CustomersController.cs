
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Customers.Create;
using Wolverine;

namespace Store.Api.Controllers;

[ApiController]
[Route("customers")]
public class Customers : ApiController
{

    private readonly IMessageBus _messageBus;

    public Customers(IMessageBus messageBus)
    {

        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createCustomerResult = await _messageBus.InvokeAsync<ErrorOr<bool>>(command);

        return createCustomerResult.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }
}