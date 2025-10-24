using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    public abstract class GameInputAction : ScriptableObject
    {
        [NonSerialized] private InputAction _inputAction;
        [NonSerialized] protected InputData InputData;

        public void Initialize(InputAction inputAction, InputData inputData)
        {
            _inputAction = inputAction;
            InputData = inputData;
        }

        public void Enable()
        {
            if (_inputAction == null)
            {
                Debug.LogWarning($"{name}: Enable() called before Initialize().");
                return;
            }

            _inputAction.started += OnAction;
            _inputAction.performed += OnAction;
            _inputAction.canceled += OnAction;
        }

        public void Disable()
        {
            if (_inputAction == null) return;

            _inputAction.started -= OnAction;
            _inputAction.performed -= OnAction;
            _inputAction.canceled -= OnAction;
        }

        public abstract void OnAction(InputAction.CallbackContext context);
    }
}