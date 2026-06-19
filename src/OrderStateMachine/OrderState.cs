namespace OrderStateMachine;

public enum OrderState
{
    Created,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}
