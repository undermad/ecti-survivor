using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private HandStateMachine handStateMachine;
        [SerializeField, Self] private Animator animator;

        private void Awake()
        {
            handStateMachine.Initialize(animator);
        }

        private void Update()
        {
            handStateMachine.Tick();
        }
        
        private void FixedUpdate()
        {
            handStateMachine.FixedTick();
        }
        
    }
}