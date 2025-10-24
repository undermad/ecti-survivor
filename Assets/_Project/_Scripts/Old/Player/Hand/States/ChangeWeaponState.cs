using Explorer._Project.Scripts.FiniteStateMachine;
using Explorer._Project.Scripts.Utils;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand.States
{
    public class ChangeWeaponState : BaseState
    {
        public static readonly int ChangeWeaponHash = Animator.StringToHash(AnimationsStatesRegistry.ChangeWeapon);
        
        public ChangeWeaponState(Animator animator, bool canStopCurrentAnimation) : base(animator, canStopCurrentAnimation)
        {
        }
        
        public override void OnEnter()
        {
            Animator.CrossFade(ChangeWeaponHash, CrossFadeDuration);
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