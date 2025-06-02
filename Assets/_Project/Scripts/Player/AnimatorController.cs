using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player
{
    public class AnimatorController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] public Animator animator;
        
        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        public void UpdateSpeed(float speed)
        {
            animator.SetFloat(SpeedHash, speed);
        }
    }
}