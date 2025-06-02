namespace Explorer._Project.Scripts.FiniteStateMachine
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}