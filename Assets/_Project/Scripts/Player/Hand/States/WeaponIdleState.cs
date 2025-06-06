using Explorer._Project.Scripts.FiniteStateMachine;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Weapon.States
{
    public class WeaponIdleState : BaseState
    {
        
        private readonly int WeaponIdleHash = Animator.StringToHash("WeaponIdle"); 
        
        public WeaponIdleState(Animator animator, bool canStopAnimation) : base(animator, canStopAnimation)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(WeaponIdleHash, CrossFadeDuration);
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void OnExit()
        {
        }
    }
}