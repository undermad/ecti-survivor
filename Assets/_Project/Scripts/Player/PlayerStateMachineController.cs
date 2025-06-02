using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.FiniteStateMachine.States;
using KBCore.Refs;

namespace Explorer._Project.Scripts.Player
{
    public class PlayerStateMachineController : ValidatedMonoBehaviour
    {
        private StateMachine _stateMachine;

        public void Initialize(PlayerController playerController, AnimatorController animatorController)
        {
            var animator = animatorController.animator;
            var movementController = playerController.movementController;
            
            var locomotion = new LocomotionState(movementController, animator);
            var dash = new DashState(movementController, animator);
            
            _stateMachine = new StateMachine();
            _stateMachine.AddTransition(locomotion, dash, new FuncPredicate(() => dash.IsDashing));
            _stateMachine.AddTransition(dash, locomotion, new FuncPredicate(() => !dash.IsDashing));
            _stateMachine.SetState(locomotion);
        }

        public void Tick() => _stateMachine.Update();
        public void FixedTick() => _stateMachine.FixedUpdate();
    }
}