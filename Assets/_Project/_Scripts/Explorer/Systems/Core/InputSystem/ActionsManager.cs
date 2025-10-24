using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    public class ActionsManager : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private InputActionAsset inputActionAsset;
        [SerializeField, Anywhere] private InputData inputData;
        [SerializeField, Anywhere] private List<GameInputAction> actions = new();

        private readonly List<GameInputAction> runtimeActions = new();
        private const string InputActionName = "Player";

        private void Awake()
        {
            foreach (var asset in actions)
            {
                if (asset == null)
                {
                    Debug.unityLogger.LogWarning("Input", "Input action asset is null");
                    continue;
                };

                var inputAction = inputActionAsset.FindAction(asset.name, throwIfNotFound: true);
                var runtime = Instantiate(asset);
                runtime.name = asset.name + " (Runtime)";
                runtime.Initialize(inputAction, inputData);
                runtimeActions.Add(runtime);
            }
        }
        
        
        private void OnEnable()
        {
            inputActionAsset.FindActionMap(InputActionName).Enable();
            foreach (var action in runtimeActions)
            {
                action.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (var action in runtimeActions)
            {
                action.Disable();
            }
            inputActionAsset.FindActionMap(InputActionName).Disable();
        }

    }
}