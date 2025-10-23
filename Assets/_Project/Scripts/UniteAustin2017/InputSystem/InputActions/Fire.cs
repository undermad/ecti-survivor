using System;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions
{
    [CreateAssetMenu(fileName = "Fire", menuName = "Input/Actions/Fire")]
    public class Fire : GameInputAction
    {

        [SerializeField, Anywhere] private GameEvent fireEvent;
        
        public override void OnAction(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Debug.unityLogger.Log("Fire!!");
                    if (InputData)
                    {
                        fireEvent.Raise();
                        InputData.IsFiring = true;
                    }
                    break;
                case InputActionPhase.Canceled:
                    if (InputData)
                    {
                        InputData.IsFiring = false;
                    }
                    break;
                case InputActionPhase.Disabled:
                case InputActionPhase.Waiting:
                case InputActionPhase.Performed:
                default:
                    break;
            }
        }
    }
}