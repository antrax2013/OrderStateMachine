namespace OrderStateMachine;

public interface ITransition
{
    OrderState StateFrom { get; init; }
    OrderState StateTo { get; init; }
    OrderEvent OrderEvent { get; init; }

    bool IsValid(Order order, OrderEvent evt) => order.State == StateFrom && OrderEvent == evt;

    void Apply(Order order) => order.Mute(StateTo);
}

public class Transition(OrderEvent orderEvent, OrderState stateFrom, OrderState stateTo) : ITransition
{
    public OrderState StateFrom { get; init; } = stateFrom;
    public OrderState StateTo { get; init; } = stateTo;
    public OrderEvent OrderEvent { get; init; } = orderEvent;
}
