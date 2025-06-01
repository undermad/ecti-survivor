using System;
using _Project.Scripts.EventBus;
using _Project.Scripts.EventBus.Events;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ValidatedMonoBehaviour
{
    [Header("References")] [SerializeField, Self]
    private Animator animator;

    [SerializeField, Self] private Rigidbody2D rigidbody;
    [SerializeField, Anywhere] private InputReader inputReader;

    [Header("Settings")] [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 100f;
    [SerializeField] private float deceleration = 100f;


    private Vector2 _inputVector;
    private Vector2 _currentVelocity;

    private void OnEnable()
    {
        inputReader.Move += OnMove;
        inputReader.Fire += OnFire;
    }

    private void OnDisable()
    {
        inputReader.Move -= OnMove;
        inputReader.Fire -= OnFire;
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (_inputVector != Vector2.zero)
        {
            _currentVelocity = Vector2.MoveTowards(
                _currentVelocity,
                _inputVector * moveSpeed,
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