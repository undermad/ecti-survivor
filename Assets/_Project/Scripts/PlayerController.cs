using System;
using _Project.Scripts.EventBus;
using _Project.Scripts.EventBus.Events;
using _Project.Scripts.StateMachine;
using _Project.Scripts.StateMachine.States;
using _Project.Scripts.Utils.Timer;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : ValidatedMonoBehaviour
{
    [Header("References")] [SerializeField, Self]
    private Animator animator;

    [SerializeField, Self] private Rigidbody2D rigidbody;
    [SerializeField, Self] private Animator cameraController;
    [SerializeField, Anywhere] private InputReader inputReader;

    [Header("Settings")] [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 100f;
    [SerializeField] private float deceleration = 100f;
    
    [Header("Dash Settings")]
    [SerializeField] private float dashCooldownDuration = 2f;
    [SerializeField] private float dashForce = 100f;


    private float dashVelocity = 1f;



    private Vector2 _inputVector;
    private Vector2 _currentVelocity;
    
    static readonly int Speed = Animator.StringToHash("Speed");

    private CountdownTimer _dashCooldown;
    private CountdownTimer _dashTimer;

    
    private StateMachine _stateMachine;

    private void Awake()
    {
        _dashCooldown = new CountdownTimer(dashCooldownDuration);
        _dashTimer = new CountdownTimer(0.3f);

        _dashTimer.OnTimerStop += () =>
        {
            dashVelocity = 1f;
            _dashCooldown.Start();
        };
        _dashTimer.OnTimerStart += () => dashVelocity = dashForce;
        
        
        
        
        
        _stateMachine = new StateMachine();
        
        //declare states
        var locomotionState = new LocomotionState(this, animator);
        var dashState = new DashState(this, animator);
        
        // Define transitions
        At(locomotionState, dashState, new FuncPredicate(() => _dashTimer.IsRunning));
        At(dashState, locomotionState, new FuncPredicate(() => _dashTimer.IsFinished));
        
        // Set initial state
        _stateMachine.SetState(locomotionState);
        
    }

    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    
    private void OnEnable()
    {
        inputReader.Move += OnMove;
        inputReader.Fire += OnFire;
        inputReader.Dash += OnDash;
    }

    private void OnDisable()
    {
        inputReader.Move -= OnMove;
        inputReader.Fire -= OnFire;
        inputReader.Dash -= OnDash;

    }

    private void Update()
    {
        var currentMagnitude = _currentVelocity.magnitude;
        animator.SetFloat(Speed, currentMagnitude);
        
        HandleTimer();
        _stateMachine.Update();
        
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void HandleTimer()
    {
        _dashTimer.Tick(Time.deltaTime);
        _dashCooldown.Tick(Time.deltaTime);
    }

    public void OnDash()
    {
        if(_dashCooldown.IsRunning) return;
        _dashTimer.Start();
    }

    public void HandleMovement()
    {
        if (_inputVector != Vector2.zero)
        {
            _currentVelocity = Vector2.MoveTowards(
                _currentVelocity,
                _inputVector * (moveSpeed * dashVelocity),
                acceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            _currentVelocity = Vector2.MoveTowards(
                _currentVelocity,
                Vector2.zero,
                deceleration * Time.fixedDeltaTime
            );
        }

        rigidbody.MovePosition(rigidbody.position + _currentVelocity * Time.fixedDeltaTime);
    }
    private void OnFire(InputActionPhase phase)
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
            default:
                return;
        }
    }
    private void OnMove(Vector2 input)
    {
        _inputVector = input.normalized;
    }
}