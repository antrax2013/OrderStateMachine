namespace OrderStateMachine.Tests;

public class StateMachineTests
{
    readonly Order orderModel = new();

    private Order GetOrder(OrderState state) => orderModel with { Id = Guid.NewGuid(), State = state };

    [Fact]
    public void Given_CreatedOrder_When_PaidEventReceived_Then_OrderBecomesPaid()
    {
        // Given
        var order = GetOrder(OrderState.Created);
        var transition = new Transition(OrderEvent.OrderPaid, OrderState.Created, OrderState.Paid);
        var stateMachine = new StateMachine([transition]);

        // When
        stateMachine.Apply(OrderEvent.OrderPaid, order);

        // Then
        Assert.Equal(OrderState.Paid, order.State);
    }

    [Fact]
    public void Given_CreatedOrder_When_CancelledEventReceived_Then_OrderBecomesCanceled()
    {
        // Given
        var order = GetOrder(OrderState.Created);
        var transition = new Transition(OrderEvent.OrderCancelled, OrderState.Created, OrderState.Cancelled);
        var stateMachine = new StateMachine([transition]);

        // When
        stateMachine.Apply(OrderEvent.OrderCancelled, order);

        // Then
        Assert.Equal(OrderState.Cancelled, order.State);
    }

    [Fact]
    public void Given_CreatedOrder_When_ShippedEventReceived_Then_InvalidTransitionErrorFired()
    {
        // Given
        var order = GetOrder(OrderState.Created);
        var transition = new Transition(OrderEvent.OrderPaid, OrderState.Created, OrderState.Paid);
        var stateMachine = new StateMachine([transition]);

        // When
        Assert.Throws<InvalidOperationException>(
            () => stateMachine.Apply(OrderEvent.OrderShipped, order)
        );
    }

    [Fact]
    public void Given_CreatedOrder_When_PaidShippedAndDeliveredReceived_Then_OrderDelivred()
    {
        // Given
        var order = GetOrder(OrderState.Created);
        var paidTransition = new Transition(OrderEvent.OrderPaid, OrderState.Created, OrderState.Paid);
        var shippedTransition = new Transition(OrderEvent.OrderShipped, OrderState.Paid, OrderState.Shipped);
        var delivredTransition = new Transition(OrderEvent.OrderDelivered, OrderState.Shipped, OrderState.Delivered);

        var stateMachine = new StateMachine([paidTransition, shippedTransition, delivredTransition]);

        // When
        stateMachine.Apply(OrderEvent.OrderPaid, order);
        stateMachine.Apply(OrderEvent.OrderShipped, order);
        stateMachine.Apply(OrderEvent.OrderDelivered, order);

        // Then
        Assert.Equal(OrderState.Delivered, order.State);
    }
}
