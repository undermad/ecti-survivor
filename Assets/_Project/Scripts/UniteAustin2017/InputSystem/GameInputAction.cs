using System;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    public abstract class GameInputAction : IAction
    {
        private readonly InputAction inputAction;
        protected readonly InputData InputData;

        protected GameInputAction(InputAction inputAction, InputData inputData)
        {
            this.inputAction = inputAction;
            this.InputData = inputData;
        }

        public void Enable()
        {
            inputAction.started += OnAction;
            inputAction.performed += OnAction;
            inputAction.canceled += OnAction;
        }

        public void Disable()
        {
            inputAction.started -= OnAction;
            inputAction.performed -= OnAction;
            inputAction.canceled -= OnAction;
        }

        public abstract void OnAction(InputAction.CallbackContext context);
    }
}