using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [CreateAssetMenu(menuName = "GAS/GameplayEffectDef", fileName = "GameplayEffectDef")]
    public class GameplayEffectDef : ScriptableObject
    {
        [Header("Meta")]
        public string DisplayName;
        public GameplayTagContainer GrantedTags = new();
        public GameplayTagContainer RequiredTargetTags = new();
        public GameplayTagContainer BlockedTargetTags = new();


        [Header("Duration & Periodic")]
        public DurationPolicy Policy = DurationPolicy.Instant;
        public ScalableFloat DurationSeconds = ScalableFloat.Constant(0);
        public bool IsPeriodic = false;
        public ScalableFloat Period = ScalableFloat.Constant(1);


        [Header("Stacking")]
        public bool CanStack = false;
        public int MaxStacks = 1;
        public bool RefreshDurationOnStack = true;


        [Header("Modifiers")]
        public List<AttributeModifier> Modifiers = new();

        public float GetDuration(float level) => Policy == DurationPolicy.Duration ? Mathf.Max(0, DurationSeconds.Evaluate(level)) : 0f;
        public float GetPeriod(float level) => Mathf.Max(0.0001f, Period.Evaluate(level));
    }
}