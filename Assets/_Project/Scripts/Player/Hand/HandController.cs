using System;
using Explorer._Project.Scripts.Player.Weapon;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HandController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private HandStateMachineController handStateMachineController;
        [SerializeField, Self] private Animator animator;
        private void Awake()
        {
            handStateMachineController.Initialize(animator);
        }

        private void Update()
        {
            handStateMachineController.Tick();
        }
        
        private void FixedUpdate()
        {
            handStateMachineController.FixedTick();
        }
    }
}