using System;
using Explorer._Scripts.Explorer.Systems.CombatSystem;
using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Components.Abilities
{
    public class AbilityActivator : MonoBehaviour
    {
        [SerializeField, Anywhere] private BaseRangeAttack rangeAttackAbility;
        [SerializeField, Self] private AbilitySystemComponent asc;
        
        private void FixedUpdate()
        {
            if (InputData.IsFiring)
            {
                asc.TryActivateAbility(rangeAttackAbility);
            }
        }
    }
}