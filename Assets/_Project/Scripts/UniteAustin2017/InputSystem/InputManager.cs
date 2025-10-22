using System;
using System.Collections.Generic;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem.Events;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem.InputActions;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    public class InputManager : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private InputActionAsset inputActionAsset;
        [SerializeField, Anywhere] private InputData inputData;
        private List<IAction> actions;
        private const string InputActionName = "Player";

        private void Awake()
        {
            actions = new List<IAction>
            {
                new Move(inputActionAsset.FindAction("Move"), inputData),
                new Fire(inputActionAsset.FindAction("Fire"), inputData)
            };
        }

        private void OnEnable()
        {
            inputActionAsset.FindActionMap(InputActionName).Enable();
            foreach (var action in actions)
            {
                action.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (var action in actions)
            {
                action.Disable();
            }
            inputActionAsset.FindActionMap(InputActionName).Disable();
        }

    }
}