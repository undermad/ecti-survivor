using System;
using Explorer._Project.Scripts.Player.Weapon;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandController : ValidatedMonoBehaviour
    {
        [FormerlySerializedAs("handStateMachineController")] [SerializeField, Self] private HandStateMachine handStateMachine;
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