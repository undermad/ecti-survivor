using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.Player.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.Player.Movement
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "The-project/Input/InputReader")]
    public class InputReader : ScriptableObject, InputActions.IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction Dash = delegate { };

        private InputActions _inputActions;

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new InputActions();
                _inputActions.Player.SetCallbacks(this);
            }

            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            EventBus<MoveEvent>.Publish(new MoveEvent{Direction = context.ReadValue<Vector2>()});
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look.Invoke(context.ReadValue<Vector2>(), context.control.device.name == "Mouse");
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            EventBus<FireButtonEvent>.Publish(new FireButtonEvent{ Phase = context.phase});
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Dash.Invoke();
            }
        }
    }
}