using System;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [Serializable]
    public class Attribute
    {
        public string Name;
        public float BaseValue = 0f;
        public float CurrentValue = 0f;
        public float Min = float.NegativeInfinity;
        public float Max = float.PositiveInfinity;


        public Attribute() {}
        public Attribute(string name, float baseValue, float min = float.NegativeInfinity, float max = float.PositiveInfinity)
        {
            Name = name; BaseValue = baseValue; CurrentValue = baseValue; Min = min; Max = max;
        }
        public void Clamp() => CurrentValue = Mathf.Clamp(CurrentValue, Min, Max);
        public void ResetToBase()
        {
            CurrentValue = BaseValue;
            Clamp(); 
        }

    }
}