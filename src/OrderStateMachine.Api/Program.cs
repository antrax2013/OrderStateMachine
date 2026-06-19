using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrderStateMachine;
using OrderStateMachine.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var stateMachine = new OrderStateMachine.Api.OrderStateMachine();
List<Order> orders = [
    new Order() { Id = -1, State = OrderState.Cancelled },
    new Order() { Id = 1 , State = OrderState.Created},
    new Order() { Id = 2, State = OrderState.Paid }
];

app.MapPost("/create/{orderId}", (int orderId, ILogger<Program> logger) =>
{
    Log.EventReceived(logger, OrderEvent.OrderCreate, orderId);

    if (!orders.Any(orders => orders.Id == orderId))
    {
        orders.Add(new() { Id = orderId });

        Log.CreateOrder(logger, orderId);

        return Results.Created();
    }

    return Results.BadRequest();
});


app.MapPut("/receive/{orderId}",
(int orderId, ReceiveRequest body, ILogger<Program> logger) =>
{
    var evt = Enum.Parse<OrderEvent>(body.Evt);

    Log.EventReceived(logger, evt, orderId);

    try
    {
        var order = orders.Single(o => o.Id == orderId);
        var initialState = order.State;

        stateMachine.Apply(evt, order);

        Log.EventCompleted(logger, evt, orderId, order.State, initialState);

        return Results.Ok();
    }
    catch (ArgumentNullException e)
    {
        Log.Error(logger, e);
        return Results.NotFound($"Order {orderId} not found");
    }
    catch (InvalidOperationException)
    {
        Log.EventFailed(logger, evt, orderId);

        return Results.BadRequest();
    }
    catch (Exception e)
    {
        Log.Error(logger, e);
        return Results.InternalServerError();
    }

});

app.Run();


public record ReceiveRequest(string Evt);