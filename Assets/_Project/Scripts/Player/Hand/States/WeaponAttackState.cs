using Explorer._Project.Scripts.FiniteStateMachine;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand.States
{
    public class WeaponAttackState : BaseState
    {
        private readonly int WeaponAttackHash = Animator.StringToHash("WeaponAttack");

        public WeaponAttackState(Animator animator, bool canStopAnimation) : base(animator, canStopAnimation)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(WeaponAttackHash, CrossFadeDuration);
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