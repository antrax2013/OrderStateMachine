using Microsoft.Extensions.Logging;

namespace OrderStateMachine.Api;

public static partial class Log
{
    [LoggerMessage(
                EventId = 1,
                Level = LogLevel.Information,
                Message = "{evt} received for order {orderId}")]
    public static partial void EventReceived(ILogger logger, OrderEvent evt, int orderId);

    [LoggerMessage(
            EventId = 2,
            Level = LogLevel.Information,
            Message = "new order {orderId} created")]
    public static partial void CreateOrder(ILogger logger, int orderId);

    [LoggerMessage(
            EventId = 3,
            Level = LogLevel.Information,
            Message = "{evt} completed for order {orderId} from {initialState} to {newState}")]
    public static partial void EventCompleted(ILogger logger, OrderEvent evt, int orderId, OrderState initialState, OrderState newState);

    [LoggerMessage(
            EventId = 4,
            Level = LogLevel.Error,
            Message = "Incompatible {evt} rejected for order {orderId}")]
    public static partial void EventFailed(ILogger logger, OrderEvent evt, int orderId);

    [LoggerMessage(
            EventId = 5,
            Level = LogLevel.Critical,
            Message = "Error {ex}")]
    public static partial void Error(ILogger logger, Exception ex);

}
