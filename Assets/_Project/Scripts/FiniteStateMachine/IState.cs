namespace Explorer._Project.Scripts.FiniteStateMachine
{
    public interface IState
    {
        bool CanStopAnimation();
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}