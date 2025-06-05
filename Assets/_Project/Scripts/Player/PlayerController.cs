using Explorer._Project.Scripts.Player;
using Explorer._Project.Scripts.Player.Movement;
using KBCore.Refs;
using UnityEngine;

public class PlayerController : ValidatedMonoBehaviour
{
    [SerializeField, Anywhere] private InputReader inputReader;
    [SerializeField, Self] public MovementController movementController;
    [SerializeField, Self] public DashAbility dashAbility;
    [SerializeField, Child] private PlayerStateMachineController stateMachineController;
    [SerializeField, Child] private Animator animator;

    private void Awake()
    {
        stateMachineController.Initialize(this, animator);
    }

    private void OnEnable()
    {
        inputReader.Dash += dashAbility.TryDash;
    }

    private void OnDisable()
    {
        inputReader.Dash -= dashAbility.TryDash;
    }

    private void Update()
    {
        stateMachineController.Tick();
    }

    private void FixedUpdate()
    {
        stateMachineController.FixedTick();
    }
}