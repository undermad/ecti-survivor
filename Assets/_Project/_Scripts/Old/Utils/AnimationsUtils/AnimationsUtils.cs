using System;
using UnityEngine;

namespace Explorer._Project.Scripts.Utils.AnimationsUtils
{
    public static class AnimationsUtils
    {

        public static bool IsInProgressBetween(Animator animator, float from, float to)
        {
            var animationStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            var normalizedTime = animationStateInfo.normalizedTime;
            var x = Math.Floor(normalizedTime);
            return normalizedTime >= x + from && normalizedTime <= x + to;
        }
    }
}