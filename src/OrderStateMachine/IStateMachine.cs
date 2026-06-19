namespace OrderStateMachine;

public interface IStateMachine
{
    public void Apply(OrderEvent evt, Order order);
}