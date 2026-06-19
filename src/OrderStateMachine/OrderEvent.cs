namespace OrderStateMachine;

public enum OrderEvent
{
    OrderCreate,
    OrderPaid,
    OrderShipped,
    OrderDelivered,
    OrderCancelled,
}
