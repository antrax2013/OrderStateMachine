namespace OrderStateMachine;

public record Order
{
    public int Id { get; init; }

    private OrderState _state;

    public OrderState State { get => _state; init => _state = value; }

    internal void Mute(OrderState state) => _state = state;
}
