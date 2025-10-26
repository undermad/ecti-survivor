using System;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Scripts.Explorer.Systems.CombatSystem.Events;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [Serializable]
    public class Attribute
    {
        [SerializeField] private string name;
        [SerializeField] private float currentValue;
        [SerializeField] private float min;
        [SerializeField] private float max;

        public string Name
        {
            get => name;
            private set => name = value;
        }

        public float CurrentValue
        {
            get => currentValue;
            private set => currentValue = value;
        }

        public float Min
        {
            get => min;
            private set => min = value;
        }

        public float Max
        {
            get => max;
            private set => max = value;
        }

        public Attribute(
            string name,
            float currentValue,
            float min = float.NegativeInfinity,
            float max = float.PositiveInfinity)
        {
            Name = name;
            CurrentValue = currentValue;
            Min = min;
            Max = max;
        }

        public void SetCurrentValue(float value, Guid owner)
        {
            Debug.unityLogger.Log($"Value: {value}, Owner: {owner}");
            var oldValue = CurrentValue;
            CurrentValue = Mathf.Clamp(value, Min, Max);
            EventBus<AttributeChangedEvent>.Publish(
                owner,
                new AttributeChangedEvent
                {
                    Owner = owner,
                    AttributeName = Name,
                    NewValue = CurrentValue,
                    OldValue = oldValue,
                    Min = Min,
                    Max = Max,
                });
        }
    }
}