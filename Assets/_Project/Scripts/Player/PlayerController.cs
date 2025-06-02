using _Project.Scripts.EventBus;
using _Project.Scripts.EventBus.Events;
using Explorer._Project.Scripts;
using Explorer._Project.Scripts.Player;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ValidatedMonoBehaviour
{
    [SerializeField, Anywhere] private InputReader inputReader;
    [SerializeField, Self] public MovementController movementController;
    [SerializeField, Self] private DashAbility dashAbility;
    [SerializeField, Self] private AnimatorController animatorController;
    [SerializeField, Self] private PlayerStateMachineController stateMachineController;

    private void Awake()
    {
        stateMachineController.Initialize(this, animatorController);
    }

    private void OnEnable()
    {
        inputReader.Move += movementController.SetInput;
        inputReader.Dash += dashAbility.TryDash;
        inputReader.Fire += HandleFire;
    }

    private void OnDisable()
    {
        inputReader.Move -= movementController.SetInput;
        inputReader.Dash -= dashAbility.TryDash;
        inputReader.Fire -= HandleFire;
    }

    private void Update()
    {
        dashAbility.TickTimers(Time.deltaTime);
        stateMachineController.Tick();
        animatorController.UpdateSpeed(movementController.GetCurrentSpeed());
    }

    private void FixedUpdate()
    {
        movementController.SetVelocityMultiplier(dashAbility.CurrentMultiplier);
        stateMachineController.FixedTick();
        movementController.FixedTick();
    }

    private void HandleFire(InputActionPhase phase)
    {
        switch (phase)
        {
            case InputActionPhase.Started:
                EventBus<TestEvent>.Publish(new TestEvent { Message = "Fire start!!" });
                break;
            case InputActionPhase.Performed:
                EventBus<TestEvent>.Publish(new TestEvent { Message = "Fire perform!!" });
                break;
            case InputActionPhase.Canceled:
                EventBus<TestEvent>.Publish(new TestEvent { Message = "Fire cancel!!" });
                break;
        }
    }
}