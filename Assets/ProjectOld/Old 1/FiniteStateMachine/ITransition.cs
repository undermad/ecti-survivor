namespace Explorer._Project.Scripts.FiniteStateMachine
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}