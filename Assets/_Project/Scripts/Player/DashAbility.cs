using System;
using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.Player.Events;
using Explorer._Project.Scripts.Utils.Timer;
using Explorer._Project.Scripts.World;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player
{
    public class DashAbility : ValidatedMonoBehaviour
    {
        [Header("Dash Settings")]
        [SerializeField] private float dashCooldownDuration = 2f;
        [SerializeField] private float dashForce = 100f;
        [SerializeField] private float dashDuration = 0.3f;
        
        public bool IsDashing { get; private set; }

        private bool CanDash { get; set; } = true;

        private CountdownTimer _cooldown;
        private CountdownTimer _duration;

        private void Awake()
        {
            _cooldown = new CountdownTimer(dashCooldownDuration);
            _duration = new CountdownTimer(dashDuration);
            
            CooldownManager.AddTimers( _cooldown, _duration);

            _duration.OnTimerStart += () =>
            {
                IsDashing = true;
                CanDash = false;
                EventBus<DashAbilityStartedEvent>.Publish(new DashAbilityStartedEvent{ DashForce = 100f });
            };

            _duration.OnTimerStop += () =>
            {
                IsDashing = false;
                _cooldown.Start();
                _duration.Reset();
                EventBus<DashAbilityEndedEvent>.Publish(new DashAbilityEndedEvent());
            };

            _cooldown.OnTimerStop += () =>
            {
                _cooldown.Reset();
                CanDash = true;
            };
        }

        public void TryDash()
        {
            if (!CanDash) return;
            _duration.Start();
        }

        private void OnDestroy()
        {
            CooldownManager.RemoveTimer(_cooldown, _duration);
        }
    }
}