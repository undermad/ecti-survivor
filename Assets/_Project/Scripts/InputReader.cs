using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "The-project/Input/InputReader")]
public class InputReader : ScriptableObject, InputActions.IPlayerActions
{
    
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction<InputActionPhase> Fire = delegate { }; 

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

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look.Invoke(context.ReadValue<Vector2>(), context.control.device.name == "Mouse");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Fire.Invoke(context.phase);
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }

}
