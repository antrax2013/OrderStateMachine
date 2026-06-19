namespace OrderStateMachine;

public class StateMachine(IEnumerable<ITransition> transitions) : IStateMachine
{
    public void Apply(OrderEvent evt, Order order)
    {
        var transition = GetTransition(evt, order.State);
        if (transition.IsValid(order, evt))
            transition.Apply(order);
    }

    private ITransition GetTransition(OrderEvent evt, OrderState state)
        => transitions.First(t => t.OrderEvent == evt && t.StateFrom == state)
            ?? throw new InvalidOperationException($"Error of transition, state order {state} incompatible with event {evt}");
}
