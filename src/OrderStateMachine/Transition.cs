namespace OrderStateMachine;

public abstract class ATransition(OrderEvent orderEvent, OrderState stateFrom, OrderState stateTo, IActionHandler? actionHandler = null) : ITransition
{
    public OrderState StateFrom { get; init; } = stateFrom;
    public OrderState StateTo { get; init; } = stateTo;
    public OrderEvent OrderEvent { get; init; } = orderEvent;

    private readonly IActionHandler? _actionHandler = actionHandler;

    public void Apply(Order order)
    {
        _actionHandler?.Handle();
        order.Mute(StateTo);
    }
}

public sealed class Transition(OrderEvent orderEvent, OrderState stateFrom, OrderState stateTo, IActionHandler? actionHandler = null) : ATransition(orderEvent, stateFrom, stateTo, actionHandler)
{
}
