using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.Player.States;
using UnityEngine;

namespace Explorer._Project.Scripts.Player
{
    public class PlayerStateMachineController : MonoBehaviour
    {
        private StateMachine _stateMachine;

        public void Initialize(PlayerController playerController, Animator animator)
        {
            var movementController = playerController.movementController;
            var dashAbility = playerController.dashAbility;
            
            
            var locomotion = new LocomotionState(movementController, animator);
            var dash = new DashState(movementController, animator);
            
            _stateMachine = new StateMachine();
            _stateMachine.AddTransition(locomotion, dash, new FuncPredicate(() => dashAbility.IsDashing));
            _stateMachine.AddTransition(dash, locomotion, new FuncPredicate(() => !dashAbility.IsDashing));
            _stateMachine.SetState(locomotion);
        }

        public void Tick() => _stateMachine.Update();
        public void FixedTick() => _stateMachine.FixedUpdate();
    }
}