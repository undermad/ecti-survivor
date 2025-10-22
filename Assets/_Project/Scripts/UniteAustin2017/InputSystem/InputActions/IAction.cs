using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions
{
    public interface IAction
    {
        void Enable();
        void Disable();
        void OnAction(InputAction.CallbackContext context);
    }
}