namespace OrderStateMachine.Api;

public class OrderStateMachine : IStateMachine
{
    private readonly StateMachine stateMachine = new([
        new Transition (OrderEvent.OrderPaid, OrderState.Created, OrderState.Paid, new Notify("=> paid event")),
        new Transition(OrderEvent.OrderShipped, OrderState.Paid, OrderState.Shipped),
        new Transition(OrderEvent.OrderDelivered, OrderState.Shipped, OrderState.Delivered),

        new Transition (OrderEvent.OrderCancelled, OrderState.Created, OrderState.Cancelled),
        new Transition (OrderEvent.OrderCancelled, OrderState.Paid, OrderState.Cancelled),
        ]);

    public void Apply(OrderEvent evt, Order order) => stateMachine.Apply(evt, order);
}
