using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Scripts.Explorer.Systems.Core.InputSystem
{
    public abstract class GameInputAction : ScriptableObject
    {
        [NonSerialized] private InputAction _inputAction;

        public void Initialize(InputAction inputAction)
        {
            _inputAction = inputAction;
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