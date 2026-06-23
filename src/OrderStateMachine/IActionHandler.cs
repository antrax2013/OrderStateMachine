namespace OrderStateMachine;

public interface IActionHandler

{
    void Handle();
}

public sealed class Notify(string actionName) : IActionHandler
{
    public void Handle()
    {
        Console.WriteLine(actionName);
    }
}