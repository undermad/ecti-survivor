using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player
{
    public class AnimatorController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] public Animator animator;
        [SerializeField, Self] public MovementController movementController;
        
        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        private void Update()
        {
            animator.SetFloat(SpeedHash, movementController.GetCurrentSpeed());
        }
    }
}